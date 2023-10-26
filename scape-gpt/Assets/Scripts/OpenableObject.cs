using UnityEngine;

public class OpenableObject : Interactable
{
    [SerializeField] protected AudioSource openedAudioSource;
    private Animator _animator;
    private Collider _openCollider;
    private bool open;

    protected override void Start(){
        base.Start();
        open = false;
        _animator = GetComponent<Animator>();
        _openCollider = GetComponent<Collider>();
    }

    public override void Accept(IVisitor v){
        v.VisitOpenable(this);
    }
    public void Open(){
        if (!open){
            _animator.SetTrigger("Open");
            open = true;
            _openCollider.enabled = false;
            PlayOpenedSound();
        }
    }

    protected void PlayOpenedSound(){
        openedAudioSource.Play();
    }
}
