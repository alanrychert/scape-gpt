using UnityEngine;

public class OpenableObject : RoomObject
{
    [SerializeField] PlayerController player;
    private bool open;

    protected override void Start(){
        base.Start();
        open = false;
    }

    public override void Accept(IVisitor v){
        v.VisitOpenable(this);
    }
    public override void TryOpen(GrabbableObject grabbable){
        if (!open){
            var _animator = GetComponent<Animator>();
            var _collider = GetComponent<Collider>();
            _animator.SetTrigger("Open");
            open = true;
            Debug.Log("abriendo");
            _collider.enabled = false;
        }
    }
}
