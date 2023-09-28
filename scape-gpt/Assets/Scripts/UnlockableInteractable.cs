using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableInteractable : OpenableObject
{
    [SerializeField] KeyObject key;
    [SerializeField] protected AudioSource audioSource;
    protected override void Start(){
        base.Start();
    }
    
    public override void TryOpen(GrabbableObject grabbable){
        if (grabbable == key){
            this.transform.Rotate(new Vector3 (0,-90,0));
            this.transform.position = this.transform.parent.transform.position - new Vector3(0.8f,0f,-0.8f);
            grabbable.SetVisibility(false);
            Destroy(grabbable);
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
}
