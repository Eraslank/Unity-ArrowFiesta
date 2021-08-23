using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IEnumerableExtensions
{
    public static T RandomItem<T>(this List<T> data)
    {
        if (data.Count <= 0)
            return default;
        return data[new System.Random().Next(data.Count)];
    }
}
