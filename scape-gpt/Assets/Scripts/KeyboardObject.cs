using UnityEngine;

public class KeyboardObject : RoomObject
{
    [SerializeField] DigitalLock digitalLock;
    [SerializeField] string key;

    protected override void Start(){
        base.Start();
    }
    public override void Visited(){
        digitalLock.Write(key);
    }
    public string GetKey(){
        return key;
    }
}
