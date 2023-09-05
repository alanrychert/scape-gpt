using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Key : GrabbableObject
{
    public override void OnFire2PressedXR(){
        Debug.Log("action2");
    }
    public override void OnFire3PressedXR(){
        Debug.Log("action3");
    }
    public override void OnJumpPressedXR(){
        Debug.Log("action4");
    }
    public override string getDescription(){
        hasBeenSeen = true;
        return "Llave oxidada";
    }
}
