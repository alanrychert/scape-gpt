using UnityEngine;

public class PushableObject : Interactable
{
    private Rigidbody _rigidBody;
    public float PushingVelocity = 15f;

    protected override void Start(){
        base.Start();
        _rigidBody = GetComponent<Rigidbody>();
    }
    public override void Accept(IVisitor v){
        v.VisitPushable(this);
    }

    public void Push(Vector3 direction){
        // Normaliza la dirección para asegurarse de que la fuerza tenga la misma intensidad en cualquier dirección.
        direction.Normalize();

        // Aplica una fuerza en la dirección especificada.
        _rigidBody.AddForce(direction * PushingVelocity, ForceMode.Impulse);
        _rigidBody.AddForce(Vector3.up, ForceMode.Impulse);
    }
}
