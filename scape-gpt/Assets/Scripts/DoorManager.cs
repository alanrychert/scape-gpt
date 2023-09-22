using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : RoomObject
{
    private readonly string keyTag = "Key";
    private bool opened;
    protected override void Start(){
        base.Start();
        opened=false;
    }
    
    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag(keyTag) && !opened){
            Debug.Log("la llave está en la puerta");
            this.transform.Rotate(new Vector3 (0,-90,0));
            this.transform.position = this.transform.parent.transform.position - new Vector3(0.8f,0f,-0.8f);
            opened=true;
        }
    }
}
