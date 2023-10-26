using UnityEngine;
public abstract class UnlockableInteractable : Interactable
{
    [SerializeField] protected KeyObject key;
    [SerializeField] protected AudioSource lockedAudioSource;
    [SerializeField] protected AudioSource openedAudioSource;
    protected override void Start(){
        base.Start();
    }

    public override void Accept(IVisitor visitor){
        visitor.VisitUnlockableInteractable(this);
    }
    
    public override void TryOpen(GrabbableObject grabbable){
        if (grabbable == key){
            grabbable.FallToTheFloor();
            grabbable.SetVisibility(false);
            OpenAction();
            PlayOpenedSound();
            key.Use();
        }
        else{
            PlayLockedSound();
        }
    }

    protected void PlayLockedSound(){
        lockedAudioSource.Play();
    }

    protected abstract void OpenAction();

    protected void PlayOpenedSound(){
        openedAudioSource.Play();
    }
}