using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GrabbableObject : RoomObject, IInteractable
{
    private bool isGrabbed;


    protected override void Start(){
        base.Start();
        isGrabbed = false;
    }
    public void OnFire1PressedXR(){
        if (isGrabbed){
            Debug.Log("va a caer");
            fallToTheFloor();
        }
        else 
            moveToPlayer();
    }
    private void moveToPlayer(){
        PlayerController player = FindObjectOfType<PlayerController>();
        transform.SetParent(player.playerHand.transform);
        transform.localPosition = new Vector3(0, 0, 0);
        isGrabbed = true;
        player.GrabObject(this);
    }
    private void fallToTheFloor(){
        float fallSpeed = 0.1f; // Ajusta esta velocidad según tus necesidades
        FindObjectOfType<PlayerController>().DropObject();
        transform.SetParent(null);
        while (transform.position.y > -2)
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        isGrabbed = false;
    }
    public abstract void OnFire2PressedXR();
    public abstract void OnFire3PressedXR();
    public abstract void OnJumpPressedXR();
}
