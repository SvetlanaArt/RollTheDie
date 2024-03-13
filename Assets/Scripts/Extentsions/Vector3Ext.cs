using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Ext
{
    /// <summary>
    /// Round Vector3 to the specified number of decimal places
    /// </summary>
    /// <param name="countOfDigits">number of decimal places</param>
    /// <returns></returns>
    public static Vector3 RoundTo(this Vector3 vector, int countOfDigits)
    {
        float ratio = Mathf.Pow(10, countOfDigits);
        vector.x = Mathf.Round(vector.x * ratio) / ratio;
        vector.y = Mathf.Round(vector.y * ratio) / ratio;
        vector.z = Mathf.Round(vector.z * ratio) / ratio;
        return vector;
    }
}

