using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 3.5f;
    private float Gravity = 10f;
    private bool hasGrabbedSomething;
    private GameObject _gazedObject;
    private GameObject _grabbedObject;
    public GameObject playerHand;
    public CameraPointerManager playerCamera;
    private readonly string grabbableTag = "Grabbable";
    private readonly string keyTag = "Key";

    private CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera.OnNewObjectDetected += HandleNewObjectDetected;
        playerCamera.OnNoObjectDetected += HandleNoObjectDetected;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
            if (Input.GetButtonDown("Fire1")){
                if (!_grabbedObject){
                    if(_gazedObject)
                        if (_gazedObject.CompareTag(grabbableTag) || _gazedObject.CompareTag(keyTag)){
                            _grabbedObject=_gazedObject;
                            _grabbedObject.transform.SetParent(playerHand.transform);
                            _grabbedObject.transform.localPosition = new Vector3(0, -0.1f, -0.1f);
                        }
                }
                else{
                    _grabbedObject.transform.localPosition = new Vector3(0, 0, 2);
                    _grabbedObject.transform.SetParent(null);
                    _grabbedObject = null;
                }
            }
            if (Input.GetButtonDown("Fire2"))
                _gazedObject?.SendMessage("OnFire2PressedXR", null, SendMessageOptions.DontRequireReceiver);
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

    private void HandleNewObjectDetected(GameObject detectedObject)
    {
        _gazedObject = detectedObject;
        Debug.Log("Object detected: " + detectedObject.name);
    }

    private void HandleNoObjectDetected()
    {
        _gazedObject = null;
        Debug.Log("lost the detected object");
    }

    private void OnDestroy()
    {
        playerCamera.OnNewObjectDetected -= HandleNewObjectDetected;
        playerCamera.OnNoObjectDetected -= HandleNoObjectDetected;
    }
}
