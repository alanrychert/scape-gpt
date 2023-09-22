using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : RoomObject
{
    private bool isGrabbed;
    [SerializeField] PlayerController player;

    protected override void Start(){
        base.Start();
        isGrabbed = false;
    }
    public override void Visited(){
        if (isGrabbed){
            Debug.Log("va a caer");
            fallToTheFloor();
        }
        else 
            moveToPlayer();
    }
    private void moveToPlayer(){
        transform.SetParent(player.playerHand.transform);
        transform.localPosition = new Vector3(0, 0, 0);
        isGrabbed = true;
        player.GrabObject(this);
    }
    private void fallToTheFloor(){
        float fallSpeed = 0.001f; // Ajusta esta velocidad según tus necesidades
        player.DropObject();
        transform.SetParent(null);
        while (transform.position.y > -2)
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        isGrabbed = false;
    }
}
