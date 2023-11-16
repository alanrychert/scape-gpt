using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObject : GrabbableObject
{
    public virtual void Use(){
        Destroy(gameObject);
    }

    public override void Accept(IVisitor v){
        v.VisitKeyObject(this);
    }

    public void TryOpen(RoomObject objToOpen)
    {
        if (objToOpen != null)
        {
            objToOpen.TryOpen(this);
        }
        else
            FallToTheFloor();
    }
}