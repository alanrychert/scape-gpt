using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfKeyObject : KeyObject
{
    [SerializeField] private Vector3 shelfScale;
    public override void Use (){
        ToShelfScale();
    }
    public void ToShelfScale(){
        transform.localScale = shelfScale;
    }
}