using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CameraPointerManager : MonoBehaviour
{
    public static CameraPointerManager Instance;
    public event Action<GameObject> OnNewObjectDetected;
    public event Action OnNoObjectDetected; 

    [SerializeField] private GameObject pointer;
    [SerializeField] private float maxDistancePointer = 4.5f;
    [Range(0,1)]
    [SerializeField] private float distancePointerObject = 0.95f;

    private const float _maxDistance = 10;


    private GameObject PlayerHand; 
    private GameObject _gazedAtObject = null;

    private readonly string interactableTag = "Interactable";
    private float scaleSize = 0.025f;
    [HideInInspector]
    public Vector3 hitPoint;

    private void Awake(){
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        pointer.transform.localScale = Vector3.one * 0.1f;
        pointer.transform.localPosition = new Vector3(0,0,maxDistancePointer);
    }

    private void Start(){
        GazeManager.Instance.OnGazeSelection += GazeSelection;
        PlayerHand = this.transform.GetChild(0).gameObject;
    }

    private void GazeSelection(){
        _gazedAtObject?.SendMessage("OnPointerClickXR", null, SendMessageOptions.DontRequireReceiver);
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            hitPoint = hit.point;
            // GameObject detected in front of the camera.
            if (_gazedAtObject != hit.transform.gameObject)
            {
                _gazedAtObject?.SendMessage("OnPointerExitXR", null, SendMessageOptions.DontRequireReceiver);
                _gazedAtObject = hit.transform.gameObject;
                _gazedAtObject.SendMessage("OnPointerEnterXR", null, SendMessageOptions.DontRequireReceiver);
                OnNewObjectDetected?.Invoke(_gazedAtObject);
                if (hit.transform.CompareTag(interactableTag))
                    GazeManager.Instance.StartGazeSelection();
            }
            if (!hit.transform.CompareTag("Untagged")) {    
                PointerOnGaze(hit.point);
            }    
            else{
                pointerOutGaze();
            }
        }
        else
        {
            // No GameObject detected in front of the camera.
            if (_gazedAtObject != null){
                //The last detected object is no more detected
                OnNoObjectDetected?.Invoke();
                _gazedAtObject?.SendMessage("OnPointerExitXR", null, SendMessageOptions.DontRequireReceiver);
                _gazedAtObject = null;
                pointerOutGaze();
            }
            
        }

    }


    private void PointerOnGaze(Vector3 hitPoint){
        float scaleFactor = scaleSize * Vector3.Distance(transform.position, hitPoint);
        pointer.transform.localScale = Vector3.one * scaleFactor;
        Vector3 pos = CalculatePointerPosition(transform.position, hitPoint, distancePointerObject);
        pointer.transform.position = pos;
    }

    private void pointerOutGaze(){
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
