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
    [SerializeField] private TextMeshProUGUI uIText;

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

    // Send a request to the OpenAI GPT-3 API and return the response as a string
    private IEnumerator SendRequest(string input, System.Action<string> onComplete)
    {

        // Set up the request body
        requestBodyChatGPT = new RequestBodyChatGPT();
        requestBodyChatGPT.model = "text-davinci-003";
        requestBodyChatGPT.prompt = input;
        requestBodyChatGPT.max_tokens = maxResponseLength;
        requestBodyChatGPT.temperature = 0;

        string JsonData = JsonUtility.ToJson(requestBodyChatGPT);

        byte[] rawData = Encoding.UTF8.GetBytes(JsonData);
        UnityWebRequest requestChatGPT = new UnityWebRequest(endpoint, "POST");

        requestChatGPT.uploadHandler = new UploadHandlerRaw(rawData);
        requestChatGPT.downloadHandler = new DownloadHandlerBuffer();

        //-H "Content-Type: application/json" \
        //-H "Authorization: Bearer $OPENAI_API_KEY" \

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
        var inputAskingSpanishResponse = input + ". Contestame en espaniol por favor.";
        Debug.Log("Hola voy a llamar a chatgpt");
        StartCoroutine(SendRequest(inputAskingSpanishResponse, (response) =>
        {
            speechTextManager.StartSpeaking(response);
            Debug.Log("Hola ya llamé a chatgpt");
            uIText.text = "response";
            //<uses-permission android:name="android.permission.INTERNET" />
        }));
    }
}

[Serializable]
public class RequestBodyChatGPT
{
    public string model;
    public string prompt;
    public int max_tokens;
    public int temperature;
}

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
[Serializable]
public class ResponseBodyChatGPT
{
    public string id ;
    public string @object ;
    public int created ;
    public string model ;
    public List<Choice> choices ;
    public Usage usage ;

    [Serializable]
    public class Choice
    {
        public string text ;
        public int index ;
        public object logprobs ;
        public string finish_reason ;
    }

    [Serializable]
    public class Usage
    {
        public int prompt_tokens ;
        public int completion_tokens ;
        public int total_tokens ;
    }
}