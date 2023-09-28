using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObject : GrabbableObject
{
    public void Dissapear(){
        Destroy(gameObject);
    }

    public override void Accept(IVisitor v){
        v.VisitKeyObject(this);
    }

    public void TryOpen(RoomObject objToOpen)
    {
        if (objToOpen != null)
        {
            objToOpen.TryOpen(this); // Llamará automáticamente al TryOpen correcto según el tipo
        }
        else
            FallToTheFloor();
    }
}