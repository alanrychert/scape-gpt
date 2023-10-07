using UnityEngine;

public class OpenableWithPassword : RoomObject
{
    [SerializeField] private PlayerController player;
    [SerializeField] private string password;
    private bool open;

    protected override void Start(){
        base.Start();
        open = false;
    }

    public void TryOpen(string inputPassword){
        if (!open && inputPassword.Equals(password)){
            open = true;
            this.transform.Rotate(new Vector3 (0,-90,0));
            this.transform.position = this.transform.parent.transform.position - new Vector3(0.8f,0f,-0.8f);
        }
    }
}
