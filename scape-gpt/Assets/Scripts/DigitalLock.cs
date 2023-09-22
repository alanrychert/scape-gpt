using UnityEngine;

public class DigitalLock : RoomObject
{
    [SerializeField] ScreenObject screen;
    [SerializeField] KeyboardObject unlockKey;
    public string unlockPassword;

    protected override void Start(){
        base.Start();
    }
    public void Write(string key){
        if (! key.Equals(unlockKey.GetKey()))
            screen.Write(key);
        else
            if (screen.getText().Equals(unlockPassword))
                Debug.Log("unlocked");
            else
                screen.Delete();
    }
}