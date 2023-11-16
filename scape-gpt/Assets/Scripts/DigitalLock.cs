using UnityEngine;

public class DigitalLock : RoomObject
{
    [SerializeField] private ScreenObject screen;
    [SerializeField] private KeyboardObject unlockKey;
    [SerializeField] private OpenableWithPassword openable;

    protected override void Start(){
        base.Start();
    }
    public void Write(string key){
        if (!key.Equals(unlockKey.GetKey())){
            screen.Write(key);
        }
        else{
            openable.TryOpen(screen.getText());
            screen.Delete();
        }
    }
}