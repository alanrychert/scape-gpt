using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    public event Action<RaycastHit> OnNewObjectDetected;
    public event Action OnNoObjectDetected; 

    [SerializeField] private GameObject pointer;
    [SerializeField] private float maxDistancePointer = 4.5f;
    [SerializeField] private float maxDistanceInteraction = 3f;
    [Range(0,1)]
    [SerializeField] private float distancePointerObject = 0.9f;

    private const float _maxDistance = 10f;
    private float scaleSize = 0.025f;
    [HideInInspector]
    public Vector3 hitPoint { get; set; }

    private void Awake(){
        pointer.transform.localScale = Vector3.one * 0.1f;
        pointer.transform.localPosition = new Vector3(0,0,maxDistancePointer);
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public RoomObject Detect()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance)) {
            PointerOnGaze(hit.point);
            if(Vector3.Distance(hit.point, transform.position) < maxDistanceInteraction){
                return hit.transform.gameObject.GetComponent<RoomObject>();
            }
            else{
                return null;
            }   
        }
        else {
            pointerOutGaze();
            return null;
        } 

    }


    public void PointerOnGaze(Vector3 hitPoint){
        float scaleFactor = scaleSize * Vector3.Distance(transform.position, hitPoint);
        pointer.transform.localScale = Vector3.one * scaleFactor;
        Vector3 pos = CalculatePointerPosition(transform.position, hitPoint, distancePointerObject);
        pointer.transform.position = pos;
    }

    public void pointerOutGaze(){
        GazeManager.Instance.CancelGazeSelection();
        pointer.transform.localScale = Vector3.one * 0.1f;
        pointer.transform.localPosition = new Vector3(0,0,maxDistancePointer);
        pointer.transform.parent.parent.transform.rotation = transform.rotation;
    }

    private Vector3 CalculatePointerPosition(Vector3 p0, Vector3 p1, float t){
        float x = p0.x + t * (p1.x - p0.x);
        float y = p0.y + t * (p1.y - p0.y);
        float z = p0.z + t * (p1.z - p0.z);
        Vector3 result = new Vector3(x,y,z);

        return result;
    }

}