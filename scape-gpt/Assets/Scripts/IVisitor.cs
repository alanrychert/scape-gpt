using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVisitor
{
    public void VisitGrabbable(GrabbableObject grabbable);
    public void VisitRoomObject(RoomObject roomObject);
    public void VisitOpenable(OpenableObject openable);
    public void VisitKeyboardObject(KeyboardObject keyboardObject);
    public void VisitPushable(PushableObject pushable);
    public void VisitKeyObject(KeyObject pushable);
    // public void VisitUnlockableInteractable(UnlockableInteractable unlockable);
}
