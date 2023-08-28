using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private readonly string keyTag = "Key";

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag(keyTag))
            Debug.Log("la llave está en la puerta");
    }
}
