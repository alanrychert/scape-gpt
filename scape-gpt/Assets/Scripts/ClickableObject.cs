using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickableObject : RoomObject
{
    public UnityEvent onClick;
    public override void Visited(){
        onClick?.Invoke();
    }
}
