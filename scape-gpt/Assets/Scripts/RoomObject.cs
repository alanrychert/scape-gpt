using System.Collections;
using UnityEngine;
using TMPro;

public abstract class RoomObject: MonoBehaviour
{
    protected bool hasBeenSeen;
    private PlayerController player;

    protected virtual void Start(){
        player = FindObjectOfType<PlayerController>();
        hasBeenSeen = false;
    }

    public void OnPointerEnterXR(){
        player.roomInformation+=getDescription();
    }

    public abstract string getDescription();
}