using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using UnityEngine.UI;

public class ArrowController : MonoBehaviourSingleton<ArrowController>
{
    public short maxVisibleArrowCount = 100;
    public int baseArrowCount = 1;
    public int arrowCount;

    [SerializeField] private float dragSensitivty = 0.1f;
    [SerializeField] private Transform arrowHolder;
    [SerializeField] private GameObject arrowPrefab;

    [SerializeField] private Text arrowCountText;

    Stack<Arrow> arrows = new Stack<Arrow>();

    PoolManager<Arrow> arrowPool;

    public float spiralUnitBase = 0.1f;
    public float spiralFreq = 3.5f;

    CameraController cc;

    public void ChangeArrowCount(int count)
    {
        if (count == arrowCount)
            return;
        else if (count < 0) // GAMEOVER
            return;

        arrowCountText.text = count.ToString();

        bool create = count > arrowCount;

        foreach (var arrow in arrows) // ReConfigure Previous Arrows
        {
            arrow.unit = spiralUnitBase / (float)(count > maxVisibleArrowCount ? maxVisibleArrowCount : count);
            arrow.SetPos();
        }

        var difference = Mathf.Abs(count - arrowCount);

        for (int i = 0; i < difference; i++)
        {
            Arrow arrow;
            if (create)
            {
                if (arrowCount < maxVisibleArrowCount)
                {
                    arrow = arrowPool.Get();
                    arrow.transform.SetParent(arrowHolder);
                    arrow.position = arrow.id = arrowCount + i;
                    arrow.unit = spiralUnitBase / count;
                    arrow.freq = spiralFreq;
                    arrow.DePool();
                    arrows.Push(arrow);
                }
                arrowCount++;
                continue;
            }
            if (arrowCount < maxVisibleArrowCount)
            {
                arrow = arrows.Pop();
                arrow.Pool();
            }

            arrowCount--;
        }
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        arrowPool = new PoolManager<Arrow>(arrowPrefab);
        for (int i = 0; i < baseArrowCount; i++)
        {
            var arrow = arrowPool.Get();
            arrow.transform.SetParent(arrowHolder);
            arrow.position = arrow.id = i;
            arrow.unit = spiralUnitBase / (float)baseArrowCount;
            arrow.freq = spiralFreq;
            arrow.DePool();
            arrows.Push(arrow);
            arrowCount++;
        }

        arrowCountText.text = baseArrowCount.ToString();

        while ((cc = CameraController.Instance) == null)
            yield return null;
    }

    private void SetPath(Vector3[] pos)
    {
        transform.DOPath(pos, pos.Length - 1, PathType.CatmullRom, PathMode.Full3D, gizmoColor: Color.red)
            .SetLookAt(0, false)
            .SetEase(Ease.Linear)
            .OnWaypointChange((id) => { CalculateRotation(id); })
            .OnComplete(MergeArrows);
    }
    public void CalculateRotation(int id)
    {
        if (id + 1 >= path.Count)
            return;
        var wayPoint = path.ElementAt(id + 1);
        cc.transform.DORotateQuaternion(wayPoint.transform.rotation, 1f).SetEase(Ease.Linear);
    }

    List<WayPoint> path;
    public void SetPath(List<WayPoint> wayPoints)
    {
        path = wayPoints;
    }


    bool didBegin = false;
    private void BeginGame()
    {
        Vector3[] pos = new Vector3[path.Count];
        for (int i = 0; i < pos.Length; i++)
            pos[i] = path[i].transform.position;
        SetPath(pos.ToArray());
    }

    public void MergeArrows()
    {
        canMove = false;
        var arr = arrows.Last();
        foreach (var arrow in arrows)
            arrow.Merge(arr != arrow);
        DOVirtual.Vector3(Vector3.one, new Vector3(arrows.Count / 20, 2, arrows.Count / 20), 5f, (val) =>
        {
            arr.transform.localScale = val;
        });

        transform.DORotate(Vector3.zero, .25f)
        .OnComplete(() =>
        {
            transform.DOMove(transform.position + cc.transform.up, .75f).SetEase(Ease.InOutBack);
        });
        arrowHolder.DOLocalMove(Vector3.zero, .5f);
    }

    bool canMove = true;
    void Update()
    {
        arrowCountText.transform.parent.rotation = cc.transform.rotation;
        if (canMove && Input.touchCount > 0)
        {
            foreach (var touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    if (!didBegin)
                    {
                        didBegin = true;
                        BeginGame();
                    }
                    var pos = arrowHolder.localPosition;
                    pos += cc.transform.right * touch.deltaPosition.x * dragSensitivty;
                    pos = Vector3.ClampMagnitude(pos, 1.5f);
                    arrowHolder.localPosition = pos;
                }
            }
        }
    }
    /*
    private void OnValidate()
    {
        foreach (var arrow in arrows)
        {
            arrow.unit = spiralUnitBase / (float)arrowCount;
            arrow.freq = spiralFreq;
            arrow.SetPos();
        }
    }
    */
}
