using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextSpeech;
using UnityEngine.Android;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;

public class SpeechTextManager : MonoBehaviour
{
    [SerializeField] private string language = "es-ES";
    [SerializeField] private TextMeshProUGUI uIText;

    // [Serializable]
    // public struct VoiceCommand
    // {
    //     public string KeyWord;
    //     public UnityEvent Response;
    // }

    // public VoiceCommand[] VoiceCommands;

    // public Dictionary<string,UnityEvent> commands = new Dictionary<string,UnityEvent>();

    public ChatGPT chatGPT;
    // public GameObject virtualAssistant;

    private void Awake()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
#endif
        // foreach(var command in VoiceCommands)
        // {
        //     commands.Add(command.KeyWord.ToLower(), command.Response);
        // }  
    }

    // Start is called before the first frame update
    public void Start()
    {
        TextToSpeech.Instance.Setting(language,1,1);
        SpeechToText.Instance.Setting(language);
        
        SpeechToText.Instance.onResultCallback = OnFinalSpeechResult;
        TextToSpeech.Instance.onStartCallBack = OnSpeakStart;
        TextToSpeech.Instance.onDoneCallback = OnSpeakStop;
        #if UNITY_ANDROID
            SpeechToText.Instance.onPartialResultsCallback = OnPartialSpeechResult;
        #endif
    }

    // Update is called once per frame
    public void StartListening()
    {
        //uIText.text = "Escuchando...";
        SpeechToText.Instance.StartRecording();
    }

    public void StopListening()
    {
        SpeechToText.Instance.StopRecording();
        // uIText.text = "Presiona para hablar";
    }

    public void OnFinalSpeechResult(string prompt)
    {
        Debug.Log("Hola esto es onfinalspeechresult");
        prompt = prompt.ToLower(); 
        // if (virtualAssistant.activeInHierarchy && prompt != "desactivar asistente"){
            chatGPT.MakeRequest(prompt);
        // }
        // if (prompt != null)
        // {
            // var response = commands[prompt];
            // if (response != null)
                // response?.Invoke();
        // }
    }

    public void OnPartialSpeechResult(string result)
    {
        // uIText.text = "onpartial";
        // uIText.text = result;
    }

    public void StartSpeaking (string message)
    {
        TextToSpeech.Instance.StartSpeak(message);
        uIText.text = "Teoricamente hablando";
    }

    public void StopSpeaking()
    {
        TextToSpeech.Instance.StopSpeak();
        uIText.text = "Teoricamente para de hablar";
    }

    public void OnSpeakStart()
    {
        Debug.Log("Talking start...");
    }

    public void OnSpeakStop()
    {
        Debug.Log("Talking stop...");
    }
}

