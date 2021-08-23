using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArrowController : MonoBehaviour
{
    public int arrowCount = 1;
    Stack<Arrow> arrows = new Stack<Arrow>();

    [SerializeField] GameObject[] test;

    // Start is called before the first frame update
    void Start()
    {
        Vector3[] pos = new Vector3[test.Length];
        for (int i = 0; i < pos.Length; i++)
            pos[i] = test[i].transform.position;
        transform.DOPath(pos, 5f, PathType.CatmullRom, PathMode.Full3D, gizmoColor: Color.red).SetLookAt(0,false).SetEase(Ease.Linear).SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
