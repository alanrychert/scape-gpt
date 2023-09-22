using System.Collections;
using UnityEngine;
using TMPro;

public class RoomObject: MonoBehaviour
{
    protected bool hasBeenSeen;
    [SerializeField] private string description;
    [SerializeField]
    private ICommand command;
    protected virtual void Start(){
        hasBeenSeen = false;
    }

    public string See(){
        string result = "";
        if (!hasBeenSeen){
            result+=description;
            hasBeenSeen = true;
            Debug.Log(description);
        }
        return result;
    }

    public virtual void Visited(){
        //command.Execute();
    }
}