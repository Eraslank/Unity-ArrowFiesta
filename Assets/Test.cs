using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private float unit = .08f;
    [SerializeField] private float freq = 3.5f;
    [SerializeField] private float objectScale = 10f;

    HashSet<Arrow> arrows = new HashSet<Arrow>();
    private void Start()
    {
        for (int i = 0; i <= 100; i++)
        {
            var arrow = GameObject.CreatePrimitive(PrimitiveType.Sphere).AddComponent<Arrow>();
            arrow.id = i;
            arrow.Set(unit / 1000f, freq, objectScale);
            arrows.Add(arrow);
        }
    }

    private void OnValidate()
    {
        if (arrows == null || arrows.Count < 1)
            return;

        foreach (var arrow in arrows)
            arrow.Set(unit/1000f, freq, objectScale);
    }

    /*
    Vector3 GetSpiralPos(int i)
    {
        return new Vector3(i * unit * Mathf.Cos(i / freq), i * unit * Mathf.Sin(i / freq));
    }
    */
}
