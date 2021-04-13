using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInput
{
    enum InputState
    {
        Active,
        OnlyShot,
        OnlyMove,
    }
    private KeyCode leftKey;
    private KeyCode rightKey;
    private KeyCode upKey;
    private KeyCode downKey;
    private KeyCode shotKey;
    private InputState inputState;
    public CustomInput(KeyCode leftKey, KeyCode rightKey, KeyCode upKey, KeyCode downKey, KeyCode shotKey)
    {
        inputState = InputState.Active;
        this.leftKey = leftKey;
        this.rightKey = rightKey;
        this.upKey = upKey;
        this.downKey = downKey;
        this.shotKey = shotKey;
    }
    public int GetVerticalAxis()
    {
        if(inputState == InputState.Active || inputState == InputState.OnlyMove)
        {
            if (Input.GetKey(upKey) && Input.GetKey(downKey))
                return 0;
            if (Input.GetKey(upKey))
                return 1;
            if (Input.GetKey(downKey))
                return -1;
        }
        return 0;
    }
    public int GetHorisontalAxis()
    {
        if (inputState == InputState.Active || inputState == InputState.OnlyMove)
        {
            if (Input.GetKey(rightKey) && Input.GetKey(leftKey))
                return 0;
            if (Input.GetKey(rightKey))
                return 1;
            if (Input.GetKey(leftKey))
                return -1;
        }
        return 0;
    }
    public KeyCode GetShotButtonKey()
    {
        return shotKey;
    }
}
