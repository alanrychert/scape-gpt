using UnityEngine;

public class OpenableObject : Interactable
{
    [SerializeField] PlayerController player;
    [SerializeField] protected AudioSource openedAudioSource;
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
            _collider.enabled = false;
            PlayOpenedSound();
        }
    }

    protected void PlayOpenedSound(){
        openedAudioSource.Play();
    }
}
