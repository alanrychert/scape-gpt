using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{

    [SerializeField] private GameObject pointer;
    [SerializeField] private float maxDistanceInteraction = 3f;
    [Range(0,1)]

    private const float _maxDistance = 10f;
    [HideInInspector]
    public Vector3 hitPoint { get; set; }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public RoomObject Detect()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance)) {
            //PointerOnGaze(hit.point);
            if(Vector3.Distance(hit.point, transform.position) < maxDistanceInteraction){
                return hit.transform.gameObject.GetComponent<RoomObject>();
            }
            else{
                return null;
            }   
        }
        else {
            return null;
        } 

    }

}
