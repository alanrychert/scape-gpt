using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void OnFire1PressedXR();

    void OnFire2PressedXR();

    void OnFire3PressedXR();

    void OnJumpPressedXR();
}
public interface ICommand
{
    public void Execute();
}
