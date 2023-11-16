using UnityEngine;

public class KeyboardObject : Interactable
{
    [SerializeField] DigitalLock digitalLock;
    [SerializeField] string key;

    protected override void Start(){
        base.Start();
    }

    public override void Accept(IVisitor v){
        v.VisitKeyboardObject(this);
    }
    public void Write(){
        digitalLock.Write(key);
    }
    public string GetKey(){
        return key;
    }
}
