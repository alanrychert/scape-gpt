using UnityEngine;
public class UnlockableInteractable : Interactable
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
        }
        else{
            PlayLockedSound();
        }
    }

    protected void PlayLockedSound(){
        lockedAudioSource.Play();
    }

    protected virtual void OpenAction(){
        this.transform.Rotate(new Vector3 (0,90,0));
        this.transform.position = this.transform.parent.transform.position - new Vector3(0.8f,0f,-0.8f);
    }

    protected void PlayOpenedSound(){
        openedAudioSource.Play();
    }
}