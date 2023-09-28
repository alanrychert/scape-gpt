using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IVisitor
{
    public float Speed = 3.5f;
    private float Gravity = 10f;
    private bool hasGrabbedSomething;
    private RoomObject _gazedObject;
    private GrabbableObject _grabbedObject;
    public GameObject playerHand;
    public ObjectDetector playerCamera;
    public SpeechTextManager speechTextManager;
    public string roomInformation { get; set; }

    private CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        // playerCamera.OnNewObjectDetected += HandleNewObjectDetected;
        // playerCamera.OnNoObjectDetected += HandleNoObjectDetected;
        roomInformation = "";
    }

    // Update is called once per frame
    void Update()
    {
        _gazedObject = playerCamera.Detect();
        roomInformation += _gazedObject?.See();
        PlayerMovement();
            if (Input.GetButtonDown("Fire1")){
                if (!_grabbedObject){
                    _gazedObject?.Accept(this);
                }
                else{
                    _grabbedObject.Accept(this);
                }
            }
            else
                if (Input.GetButtonDown("Fire2"))
                    speechTextManager.StopSpeaking();
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

    private void GazeSelection(){
        _gazedObject?.SendMessage("OnPointerClickXR", null, SendMessageOptions.DontRequireReceiver);
    }

    public void GrabObject(GrabbableObject obj){
        _grabbedObject = obj;
    }
    public void DropObject(){
        _grabbedObject = null;
    }

    public bool HasGrabbedSomething(){
        return hasGrabbedSomething;
    }
    public void VisitKeyObject(KeyObject keyObject){
        if (!keyObject.IsGrabbed()){
            keyObject.MoveToObject(playerHand);
            _grabbedObject = keyObject;
        }
        else
        {
            Debug.Log(_gazedObject);
            keyObject.TryOpen(_gazedObject);
        }
    }

    public void VisitGrabbable(GrabbableObject grabbable){
        if (!grabbable.IsGrabbed()){
            grabbable.MoveToObject(playerHand);
            _grabbedObject = grabbable;
        }
        else
        {
            grabbable.FallToTheFloor();
            _grabbedObject = null;
        }
    }
    public void VisitRoomObject(RoomObject roomObject){}
    public void VisitOpenable(OpenableObject openable){
        openable.TryOpen(_grabbedObject);
    }
    public void VisitKeyboardObject(KeyboardObject keyboardObject){
        keyboardObject.Write();
    }
    public void VisitPushable(PushableObject pushable){
        pushable.push();
    }
}
