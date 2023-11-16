using UnityEngine;
public class UnlockableInteractableDoor : UnlockableInteractable
{
    protected override void OpenAction(){
        this.transform.Rotate(new Vector3 (0,90,0));
        this.transform.position = this.transform.parent.transform.position - new Vector3(0.8f,0f,-0.8f);
    }
}