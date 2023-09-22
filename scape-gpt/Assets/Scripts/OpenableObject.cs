using UnityEngine;

public class OpenableObject : RoomObject
{
    [SerializeField] PlayerController player;
    private Animator _animator;
    private Collider _collider;
    private bool open;

    protected override void Start(){
        base.Start();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider>();
        open = false;
    }
    public override void Visited(){
        if (!open){
            _animator.SetTrigger("Open");
            open = true;
            Debug.Log("abriendo");
            _collider.enabled = false;
        }
    }
}
