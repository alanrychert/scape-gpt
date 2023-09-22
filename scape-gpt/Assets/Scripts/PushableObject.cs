using UnityEngine;

public class PushableObject : RoomObject
{
    [SerializeField] PlayerController player;
    public float PushingVelocity = 4f;

    protected override void Start(){
        base.Start();
    }
    public override void Visited(){
        Vector3 direction = (transform.position - player.transform.position);
        direction.y = 0;
        transform.Translate(direction.normalized * PushingVelocity * Time.deltaTime);
    }
}