using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WriteableBook : UnlockableInteractable
{
    [SerializeField] TextMeshProUGUI FirstPageText;
    [SerializeField] TextMeshProUGUI SecondPageText;

    protected override void Start(){
        FirstPageText.enabled = false;
        SecondPageText.enabled = false;
    }
    protected override void OpenAction(){
        FirstPageText.enabled = true;
        SecondPageText.enabled = true;
    }
}
