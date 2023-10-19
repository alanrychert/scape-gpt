using System.Collections;
using UnityEngine;
using TMPro;

public class RoomObject: MonoBehaviour
{
    protected bool hasBeenSeen;
    [SerializeField] private string description;
    protected Renderer objectRenderer;
    protected virtual void Start(){
        hasBeenSeen = false;
        objectRenderer = GetComponent<Renderer>();
    }

    public virtual void Accept(IVisitor v){
        v.VisitRoomObject(this);
    }

    public virtual void See(IVisitor v){
        v.SeeRoomObject(this);
    }

    public string GetDescription(){
        string result = "";
        if (!hasBeenSeen){
            result+=description;
            hasBeenSeen = true;
            Debug.Log(description);
        }
        return result;
    }

    public virtual void TryOpen(GrabbableObject grabbable)
    {
        grabbable.FallToTheFloor();
    }

    public void SetVisibility(bool isVisible)
    {
        objectRenderer.enabled = isVisible;
    }
}