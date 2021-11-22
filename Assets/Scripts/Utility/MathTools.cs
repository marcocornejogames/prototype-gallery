using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Marco Cornejo, November 17th 2021
public static class MathTools
{
    public static float Remap(this float from, float fromMin, float fromMax, float toMin, float toMax) //From forum.unity.com
    {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;
    }

    public static Vector3 GetDirectionNormalized(Vector3 origin, Vector3 destination)
    {
        return (destination - origin).normalized;
    }
}
