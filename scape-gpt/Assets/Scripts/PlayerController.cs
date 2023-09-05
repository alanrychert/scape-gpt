using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 3.5f;
    private float Gravity = 10f;
    private bool hasGrabbedSomething;
    private GameObject _gazedObject;
    private GrabbableObject _grabbedObject;
    public GameObject playerHand;
    public CameraPointerManager playerCamera;
    private readonly string interactableTag = "Interactable";
    public string roomInformation { get; set; }

    private CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera.OnNewObjectDetected += HandleNewObjectDetected;
        playerCamera.OnNoObjectDetected += HandleNoObjectDetected;
        GazeManager.Instance.OnGazeSelection += GazeSelection;
        roomInformation = "";
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
            if (Input.GetButtonDown("Fire1")){
                if (!_grabbedObject){
                    Debug.Log(_grabbedObject);
                    _gazedObject?.SendMessage("OnFire1PressedXR", this, SendMessageOptions.DontRequireReceiver);
                }
                else{
                    Debug.Log(_grabbedObject);
                    _grabbedObject?.SendMessage("OnFire1PressedXR", this, SendMessageOptions.DontRequireReceiver);
                    
                }
                // if (!_grabbedObject){
                //     if(_gazedObject)
                //         if (_gazedObject.CompareTag(grabbableTag) || _gazedObject.CompareTag(keyTag)){
                //             _grabbedObject=_gazedObject;
                //             _grabbedObject.transform.SetParent(playerHand.transform);
                //             _grabbedObject.transform.localPosition = new Vector3(0, -0.1f, -0.1f);
                //         }
                // }
                // else{
                //     _grabbedObject.transform.localPosition = new Vector3(0, 0, 2);
                //     _grabbedObject.transform.SetParent(null);
                //     _grabbedObject = null;
                // }
            }
            if (Input.GetButtonDown("Fire2"))
                _gazedObject?.SendMessage("OnFire2PressedXR", this, SendMessageOptions.DontRequireReceiver);
            if (Input.GetButtonDown("Fire3"))
                _gazedObject?.SendMessage("OnFire3PressedXR", null, SendMessageOptions.DontRequireReceiver);
            if (Input.GetButtonDown("Jump"))
                _gazedObject?.SendMessage("OnJumpPressedXR", null, SendMessageOptions.DontRequireReceiver);
    }

    void PlayerMovement(){
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0 ,vertical);
        Vector3 velocity = direction * Speed;
        velocity = Camera.main.transform.TransformDirection(velocity);
        velocity.y -= Gravity;
        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleNewObjectDetected(RaycastHit detected)
    {
        if (_grabbedObject && _grabbedObject != _gazedObject || !_grabbedObject){
            if (_gazedObject != detected.transform.gameObject){
                _gazedObject?.SendMessage("OnPointerExitXR", this, SendMessageOptions.DontRequireReceiver);
                _gazedObject = detected.transform.gameObject;
                _gazedObject.SendMessage("OnPointerEnterXR", null, SendMessageOptions.DontRequireReceiver);
                Debug.Log("new object detected: ");
                if (_gazedObject.CompareTag(interactableTag))
                    GazeManager.Instance.StartGazeSelection();
                else
                    GazeManager.Instance.CancelGazeSelection();
            }
            if (!detected.transform.CompareTag("Untagged")) {    
                playerCamera.PointerOnGaze(detected.point);
            }    
            else{
                playerCamera.pointerOutGaze();
            }
        }
    }

    private void GazeSelection(){
        _gazedObject?.SendMessage("OnPointerClickXR", null, SendMessageOptions.DontRequireReceiver);
    }

    private void HandleNoObjectDetected()
    {
        if (_gazedObject != null){
                _gazedObject?.SendMessage("OnPointerExitXR", null, SendMessageOptions.DontRequireReceiver);
                _gazedObject = null;
                playerCamera.pointerOutGaze();
        }
    }

    public void GrabObject(GrabbableObject obj){
        _grabbedObject = obj;
    }
    public void DropObject(){
        _grabbedObject = null;
    }
    private void OnDestroy()
    {
        playerCamera.OnNewObjectDetected -= HandleNewObjectDetected;
        playerCamera.OnNoObjectDetected -= HandleNoObjectDetected;
    }
}
