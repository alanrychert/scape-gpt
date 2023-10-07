using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WriteableBook : UnlockableInteractable
{
    [SerializeField] TextMeshProUGUI text ;

    protected override void Start(){
        text.enabled = false;
    }
    protected override void OpenAction(){
        text.enabled = true;
    }
}
