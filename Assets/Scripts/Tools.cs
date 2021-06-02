using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class Tools
{
    public static Vector3 RejectVector(this Vector3 vec, float angleZ)
    {
        float x = vec.x * Mathf.Cos(angleZ) - vec.y * Mathf.Sin(angleZ);
        float y = vec.x * Mathf.Sin(angleZ) + vec.y * Mathf.Cos(angleZ);
        vec = new Vector3(x, y);
        return vec;
    }
}

[Serializable]
public class KeyValue<KeyT, ValueT>
{
    public KeyT Key;
    public ValueT Value;

}
