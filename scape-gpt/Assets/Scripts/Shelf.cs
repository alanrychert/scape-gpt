using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shelf : UnlockableInteractable
{
    protected override void OpenAction(){
        key.MoveToObject(this.gameObject);
        key.ToShelfScale();
        key.SetVisibility(true);
    }
}
