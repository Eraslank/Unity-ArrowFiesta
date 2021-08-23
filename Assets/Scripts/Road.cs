using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Road : MonoBehaviour
{

    [SerializeField] Transform tailTransform;

    public Road head = null, tail = null;

    public int roadId = 0;


    public WayPoint[] wayPoints;
    [SerializeField] bool autoDetectWayPoints = false;

    public void AttachToTail(Road r)
    {
        r.transform.position = tailTransform.position;
        r.transform.rotation = tailTransform.rotation;
        tail = r;
        r.head = this;
    }
    private void OnValidate()
    {
        if (!autoDetectWayPoints)
            return;
        autoDetectWayPoints = false;
        wayPoints = GetComponentsInChildren<WayPoint>();

        wayPoints.OrderBy(w => w.id);
    }
}
