using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : RoomObject
{
    private bool isGrabbed;
    private Vector3 originalRotation;
    private Rigidbody _rigidBody;
    [SerializeField] PlayerController player;

    protected override void Start(){
        base.Start();
        isGrabbed = false;
        _rigidBody = GetComponent<Rigidbody>();
    }
    public override void Accept(IVisitor v){
        v.VisitGrabbable(this);
    }
    public void MoveToObject(GameObject obj) {
        _rigidBody.useGravity = false;
        _rigidBody.isKinematic = true; 
        isGrabbed = true;
        transform.SetParent(obj.transform);
        transform.localPosition = Vector3.zero;
    }

    public void FallToTheFloor() {
        player.DropObject();
        // Desactiva isKinematic para que la gravedad afecte al objeto
        isGrabbed = false;
        transform.SetParent(null);
        _rigidBody.isKinematic = false;
        _rigidBody.WakeUp();

        // Puedes restaurar la velocidad lineal y angular del Rigidbody si es necesario

        // Hacer que el objeto vuelva a caer aplicando una fuerza hacia abajo
        _rigidBody.AddForce(player.playerCamera.transform.forward*3f + Vector3.up , ForceMode.Impulse);

        // Deja que la gravedad haga su trabajo
        _rigidBody.useGravity = true;
    }
    public bool IsGrabbed(){
        return isGrabbed;
    }
}
