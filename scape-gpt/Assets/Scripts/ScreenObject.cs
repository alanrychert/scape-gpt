using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;


public class ScreenObject : RoomObject
{
    [SerializeField] TextMeshProUGUI uIText;
    public int maxLength;
    private string screenText;

    protected override void Start(){
        base.Start();
    }
    public override void Visited(){
        //try to unlock
    }
    public void Write(string key){
        if (uIText.text.Length < maxLength){
            screenText += key;
            uIText.text = screenText;
        }
    }
    public void Delete(){
        screenText = "";
        uIText.text = screenText;
    }
    public string getText(){
        return screenText;
    }
}