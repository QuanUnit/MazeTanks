using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInput
{
    public enum InputState
    {
        Active,
        OnlyShot,
        OnlyMove,
        Disabled,
    }
    private KeyCode leftKey;
    private KeyCode rightKey;
    private KeyCode upKey;
    private KeyCode downKey;
    private KeyCode shotKey;

    private InputState inputState;

    private float horisontalInterpolationValue = 0f;
    private float verticalInterpolationValue = 0f;
    private float countOfSteps = 10;
    private float deferance;

    public CustomInput(KeyCode leftKey, KeyCode rightKey, KeyCode upKey, KeyCode downKey, KeyCode shotKey)
    {
        inputState = InputState.Active;
        this.leftKey = leftKey;
        this.rightKey = rightKey;
        this.upKey = upKey;
        this.downKey = downKey;
        this.shotKey = shotKey;
        deferance = 1 / countOfSteps;
    }
    private void DecrementInterpolationValue(ref float value)
    {
        if (value > 0)
            value -= deferance;
    }
    private void IncrementInterpolationValue(ref float value)
    {
        if (value < 1)
            value += deferance;
    }
    public float GetVerticalAxis()
    {
        float dir = 0;
        if (inputState == InputState.Active || inputState == InputState.OnlyMove)
        {
            if (Input.GetKey(downKey) && Input.GetKey(upKey))
            {
                DecrementInterpolationValue(ref verticalInterpolationValue);
                return Mathf.Lerp(0f, dir, verticalInterpolationValue);
            }
            else if (Input.GetKey(upKey))
            {
                dir = 1;
                IncrementInterpolationValue(ref verticalInterpolationValue);
                return Mathf.Lerp(0f, dir, verticalInterpolationValue);
            }
            else if (Input.GetKey(downKey))
            {
                dir = -1;
                IncrementInterpolationValue(ref verticalInterpolationValue);
                return Mathf.Lerp(0f, dir, verticalInterpolationValue);
            }
        }
        DecrementInterpolationValue(ref verticalInterpolationValue);
        return Mathf.Lerp(0f, dir, verticalInterpolationValue);
    }
    public float GetHorisontalAxis()
    {
        float dir = 0;
        if (inputState == InputState.Active || inputState == InputState.OnlyMove)
        {
            if (Input.GetKey(rightKey) && Input.GetKey(leftKey))
            {
                DecrementInterpolationValue(ref horisontalInterpolationValue);
                return Mathf.Lerp(0f, dir, horisontalInterpolationValue);
            }
            else if (Input.GetKey(rightKey))
            {
                dir = 1;
                IncrementInterpolationValue(ref horisontalInterpolationValue);
                return Mathf.Lerp(0f, dir, horisontalInterpolationValue);
            }
            else if (Input.GetKey(leftKey))
            {
                dir = -1;
                IncrementInterpolationValue(ref horisontalInterpolationValue);
                return Mathf.Lerp(0f, dir, horisontalInterpolationValue);
            }
        }
        DecrementInterpolationValue(ref horisontalInterpolationValue);
        return Mathf.Lerp(0f, dir, horisontalInterpolationValue);
    }
    public void SwitchState(InputState state)
    {
        inputState = state;
    }
    public KeyCode GetShotButtonKey()
    {
        if(inputState == InputState.Active || inputState == InputState.OnlyShot)
            return shotKey;
        return KeyCode.None;
    }
}

