using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class ArrowController : MonoBehaviourSingleton<ArrowController>
{
    public int baseArrowCount = 1;
    public int ArrowCount { get => arrows.Count; private set { } }

    [SerializeField] private float dragSensitivty = 0.1f;
    [SerializeField] private Transform arrowHolder;
    [SerializeField] private GameObject arrowPrefab;

    Stack<Arrow> arrows = new Stack<Arrow>();

    PoolManager<Arrow> arrowPool;

    [SerializeField] GameObject[] test;

    public float spiralUnitBase = 0.1f;
    public float spiralFreq = 3.5f;

    public void ChangeArrowCount(int count)
    {
        if (count == ArrowCount)
            return;

        bool create = count > ArrowCount;

        var arrowCount = ArrowCount;

        foreach (var arrow in arrows) // ReConfigure Previous Arrows
        {
            arrow.unit = spiralUnitBase / count;
        }

        for (int i = 0; i < Mathf.Abs(count - arrowCount); i++)
        {
            Arrow arrow;
            if (create)
            {
                arrow = arrowPool.Get();
                arrow.transform.SetParent(arrowHolder);
                arrow.position = arrow.id = arrowCount + i;
                arrow.unit = spiralUnitBase / count;
                arrow.freq = spiralFreq;
                arrow.DePool();
                arrows.Push(arrow);
                continue;
            }

            arrow = arrows.Pop();
            arrow.Pool();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        arrowPool = new PoolManager<Arrow>(arrowPrefab);
        for (int i = 0; i < baseArrowCount; i++)
        {
            var arrow = arrowPool.Get();
            arrow.transform.SetParent(arrowHolder);
            arrow.position = arrow.id = i;
            arrow.unit = spiralUnitBase/(float)baseArrowCount;
            arrow.freq = spiralFreq;
            arrow.DePool();
            arrows.Push(arrow);
        }
    }

    public void SetPath(Vector3[] pos)
    {
        transform.DOPath(pos, pos.Length-1, PathType.CatmullRom, PathMode.Full3D, gizmoColor: Color.red)
            .SetLookAt(0, false)
            .SetEase(Ease.Linear)
            .SetLoops(-1)
            .OnWaypointChange((id)=> { CalculateRotation(id); });
    }
    public void CalculateRotation(int id)
    {
        var wayPoint = path.ElementAt(id);
        CameraController.Instance.transform.DORotateQuaternion(wayPoint.transform.rotation, .5f);
    }

    List<WayPoint> path;
    public void SetPath(List<WayPoint> wayPoints)
    {
        path = wayPoints;
        Vector3[] pos = new Vector3[wayPoints.Count];
        for (int i = 0; i < pos.Length; i++)
            pos[i] = wayPoints[i].transform.position;
        SetPath(pos.ToArray());
    }

    public void Test()
    {
        foreach (var arrow in arrows)
            arrow.Merge();
        var arr = arrows.Last();
        DOVirtual.Vector3(Vector3.one, new Vector3(arrows.Count / 20, 2, arrows.Count / 20), 5f, (val) =>
         {
             arr.transform.localScale = val;
         });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (var touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    arrowHolder.localPosition = new Vector3(Mathf.Clamp(arrowHolder.localPosition.x + touch.deltaPosition.x * dragSensitivty, -2, 2), 0, 0);
                }
            }
        }
    }

    private void OnValidate()
    {
        foreach (var arrow in arrows)
        {
            arrow.unit = spiralUnitBase / (float)ArrowCount;
            arrow.freq = spiralFreq;
            arrow.SetPos();
        }
    }
}
