using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField] private float maxDistanceInteraction = 3f;
    [SerializeField] private GameObject pointer;
    [Range(0,1)]

    private const float _maxDistance = 10f;
    [SerializeField] private TextMeshProUGUI interactionText;
    private Vector3 hitPoint { get; set; }
    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public RoomObject Detect()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        Physics.defaultSolverIterations = 12;
        RaycastHit hit;
        if (Physics.Raycast(pointer.transform.position, transform.forward, out hit, _maxDistance)) {
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

    public void ShowInteractText(bool show){
        interactionText.enabled = show;
    }

}
