using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shelf : UnlockableInteractable
{
    [SerializeField] TextMeshProUGUI text;

    protected override void OpenAction(){
        key.MoveToObject(this.gameObject);
        key.SetVisibility(true);
        text.enabled = true;
    }
}
