using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class ChatGPT : MonoBehaviour
{
    public SpeechTextManager speechTextManager;
    [SerializeField] PlayerController player;
    //[SerializeField] private TextMeshProUGUI uIText;

    // Replace with your own OpenAI API key
    // vieja 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
    //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
    private string apiKey = "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";

    // The endpoint for the OpenAI GPT-3 API
    private string endpoint = "https://api.openai.com/v1/completions";

    // The maximum length of the response from the API
    private int maxResponseLength = 256;

    private RequestBodyChatGPT requestBodyChatGPT;
    private ResponseBodyChatGPT responseBodyChatGPT;
    private string promptHeader = "En este momento vas a actuar como si fueras el dueño de una sala de escape, que da pistas de no más de 10 palabras. Para responder a esta consulta solo podes tener en cuenta la informacion que te daré a continuacion:";

    // Send a request to the OpenAI GPT-3 API and return the response as a string
    private IEnumerator SendRequest(List<Message> messages, System.Action<string> onComplete)
    {
        requestBodyChatGPT = new RequestBodyChatGPT();
        requestBodyChatGPT.model = "text-davinci-003";
        requestBodyChatGPT.messages = messages;

        string JsonData = JsonUtility.ToJson(requestBodyChatGPT);

        byte[] rawData = Encoding.UTF8.GetBytes(JsonData);
        UnityWebRequest requestChatGPT = new UnityWebRequest(endpoint, "POST");

        requestChatGPT.uploadHandler = new UploadHandlerRaw(rawData);
        requestChatGPT.downloadHandler = new DownloadHandlerBuffer();

        requestChatGPT.SetRequestHeader("Content-Type", "application/json");
        requestChatGPT.SetRequestHeader("Authorization", "Bearer " + apiKey);

        yield return requestChatGPT.SendWebRequest();
        
        if (requestChatGPT.result == UnityWebRequest.Result.Success){
            responseBodyChatGPT = JsonUtility.FromJson<ResponseBodyChatGPT>(requestChatGPT.downloadHandler.text);
            var result = responseBodyChatGPT.choices[0].text;
            onComplete(result);
        }
        else
        {
            Debug.LogError("Error sending request to ChatGPT API: " + requestChatGPT.result);
        }
        
        requestChatGPT.Dispose();
    }

    public void MakeRequest(string input)
    {
        var inputAskingSpanishResponse = input + ", y contestame en espaniol por favor.";
        
        List<Message> messages = new List<Message>();
        
        Message systemMessage = new Message();
        systemMessage.role = "system";
        systemMessage.content = promptHeader + "En la habitación se encuentran los siguientes objetos:" + player.roomInformation;
        
        Message userMessage = new Message();
        userMessage.role = "user";
        userMessage.content = inputAskingSpanishResponse;

        messages.Add(systemMessage);
        messages.Add(userMessage);

        StartCoroutine(SendRequest(messages, (response) =>
        {
            speechTextManager.StartSpeaking(response);
            Debug.Log("Hola ya llamé a chatgpt");
    //        uIText.text = "response";
            //<uses-permission android:name="android.permission.INTERNET" />
        }));
    }

    [Serializable]
    public class RequestBodyChatGPT
    {
        public string model;
        public List<Message> messages;
    }

    [Serializable]
    public class Message
    {
        public string role;
        public string content;
    }

    [Serializable]
    public class ResponseBodyChatGPT
    {
        public string id ;
        public string @object ;
        public int created ;
        public string model ;
        public List<Choice> choices ;
    }

    [Serializable]
    public class Choice
    {
        public string text ;
    }
}