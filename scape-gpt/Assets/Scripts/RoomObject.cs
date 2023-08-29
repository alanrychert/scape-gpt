using System.Collections;
using UnityEngine;
using TMPro;

public abstract class RoomObject: MonoBehaviour
{
    private PlayerController player;

    void Start(){
        player = GetComponent<PlayerController>();
    }

    public void OnPointerEnterXR(){
        player.roomInformation+=getDescription();
    }

    public abstract string getDescription();
}