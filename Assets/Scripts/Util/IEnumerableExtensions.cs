using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IEnumerableExtensions
{
    static System.Random rand = new System.Random();
    public static T RandomItem<T>(this List<T> data)
    {
        if (data.Count <= 0)
            return default;
        return data[rand.Next(data.Count)];
    }
}
