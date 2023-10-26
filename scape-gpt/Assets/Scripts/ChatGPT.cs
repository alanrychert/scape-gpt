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

    private RequestBodyChatGPT requestBodyChatGPT;
    private ResponseBodyChatGPT responseBodyChatGPT;
    private string promptHeader = "Contexto: Actuaras como el acompañante de un jugador que quiere resolver esta sala de escape. \n";
    private string promptPossibleActionsList= "Es importante recalcar que el jugador solo puede agarrar, soltar, empujar, abrir, y colocar objetos en otros. No puede hacer ninguna otra acción. No se puede interactuar con todos los objetos. \n";
    private string promptObjectListStart = "El jugador solo vio los objetos listados a continuación, si aparece un mismo objeto más de una vez, es porque el jugador vio más de uno en la sala: Caja ";
    private string promptTail = "Dado que no sabes qué interacciones se puede realizar con cada objeto, sientete libre de sugerir cosas sensatas, pero nunca asegures que el jugador puede hacer algo con un objeto específico.\n";
    private string playerQuestionHeader = "Tu tarea como acompañante: basandote en el contexto dado pero sin mencionarlo explicitamente. IMPORTANTE: respondele al jugador, no a la totalidad de este prompt. La consulta del jugador es:\n";
    // Send a request to the OpenAI GPT-3 API and return the response as a string
    private IEnumerator SendRequest(string input, System.Action<string> onComplete)
    {
        requestBodyChatGPT = new RequestBodyChatGPT();
        requestBodyChatGPT.model = "gpt-3.5-turbo-instruct";
        requestBodyChatGPT.prompt = input;
        requestBodyChatGPT.max_tokens = 512;
        requestBodyChatGPT.temperature = 1f;

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

    public void MakeRequest(string playerQuestion)
    {
        string requestInput = promptHeader + promptPossibleActionsList + promptObjectListStart + player.roomInformation + "\n" + playerQuestionHeader + playerQuestion + promptTail;
        Debug.Log("Hola la consulta entera fue: " +requestInput);
        Debug.Log("casualmente lo que vio el usuario es: "+player.roomInformation);
        StartCoroutine(SendRequest(requestInput, (response) =>
        {
            speechTextManager.StartSpeaking(response);
            Debug.Log("Hola la respuesta entera fue: " +response);
        }));
    }

    [Serializable]
    public class RequestBodyChatGPT
    {
        public string model;
        public string prompt;
        public int max_tokens;
        public float temperature;
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
        public object logprobs;
        public int index;
        public string finish_reason;
        public string text;
    }
}