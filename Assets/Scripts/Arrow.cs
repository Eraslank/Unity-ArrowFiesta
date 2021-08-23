using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int id;
    public float unit;
    public float freq;

    public void Set(float unit, float freq,float objectScale)
    {
        transform.localScale = Vector3.one * objectScale;
        this.unit = unit;
        this.freq = freq;
        transform.position = new Vector3(id * unit * Mathf.Cos(id / freq), id * unit * Mathf.Sin(id / freq));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
