using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable: RoomObject
{
    public override void See(IVisitor visitor){
        visitor.SeeInteractable(this);
    }
}
