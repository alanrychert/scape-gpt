using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableInteractable : OpenableObject
{
    [SerializeField] protected KeyObject key;
    [SerializeField] protected AudioSource audioSource;
    protected override void Start(){
        base.Start();
    }
    
    public override void TryOpen(GrabbableObject grabbable){
        if (grabbable != null && grabbable == key){
            grabbable.FallToTheFloor();
            grabbable.SetVisibility(false);
            OpenAction();
            //Destroy(grabbable);
        }
        else
            PlayLockedSound();
    }

    protected void PlayLockedSound(){
        if (audioSource != null && audioSource.clip != null)
        {
            // Reproduce el sonido si el AudioSource y el clip están configurados
            audioSource.Play();
        }
    }

    protected virtual void OpenAction(){
        this.transform.Rotate(new Vector3 (0,90,0));
        this.transform.position = this.transform.parent.transform.position - new Vector3(0.8f,0f,-0.8f);
    }
}
