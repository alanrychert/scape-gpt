using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GrabbableObject : RoomObject, IInteractable
{
    private bool isGrabbed;

    void Start(){
        isGrabbed=false;
    }
    public void Action1(){
        isGrabbed=!isGrabbed;
    }
    public abstract void Action2();
    public abstract void Action3();
    public abstract void Action4();
}
