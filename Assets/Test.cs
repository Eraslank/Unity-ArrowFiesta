using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private float unit = 5f;
    [SerializeField] private float freq = 10f;
    private void Start()
    {
        for (int i = 0; i <= 100; i++)
            GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = GetSpiralPos(i);
    }

    Vector3 GetSpiralPos(int i)
    {
        return new Vector3(i*unit * Mathf.Cos(i/freq), i*unit * Mathf.Sin(i/freq));
    }
}
