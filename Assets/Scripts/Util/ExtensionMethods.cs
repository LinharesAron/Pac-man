using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static bool IsOpposite(this Vector3 _this, Vector3 toCompare)
    {
        var diff = toCompare - _this;
        diff.Normalize();
        float dot = Vector3.Dot(_this, diff);
        return dot == -1;
    }
}
