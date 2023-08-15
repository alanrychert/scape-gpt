using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIElementXR : MonoBehaviour
{
    public UnityEvent onXRPointerEnter;
    public UnityEvent onXRPointerExit;
    private Camera XRCamera;
    // Start is called before the first frame update
    void Start()
    {
        XRCamera = CameraPointerManager.Instance.gameObject.GetComponent<Camera>();
            }

    // Update is called once per frame
    public void OnPointerClickXR(){
        PointerEventData pointerEvent = PlacePointer();
        ExecuteEvents.Execute(this.gameObject, pointerEvent, ExecuteEvents.pointerClickHandler); 
    }

    public void OnPointerEnterXR(){
        GazeManager.Instance.SetUpGaze(1.5f);
        onXRPointerEnter?.Invoke();
        PointerEventData pointerEvent = PlacePointer();
        ExecuteEvents.Execute(this.gameObject, pointerEvent, ExecuteEvents.pointerDownHandler); 
    }

    public void OnPointerExitXR(){
        GazeManager.Instance.SetUpGaze(2.5f);
        onXRPointerExit?.Invoke();
        PointerEventData pointerEvent = PlacePointer();
        ExecuteEvents.Execute(this.gameObject, pointerEvent, ExecuteEvents.pointerUpHandler); 
    }

    private PointerEventData PlacePointer(){
        Vector3 screenPos = XRCamera.WorldToScreenPoint(CameraPointerManager.Instance.hitPoint);
        var pointer = new PointerEventData(EventSystem.current);
        pointer.position = new Vector2(screenPos.x, screenPos.y);
        return pointer;
    }
}
