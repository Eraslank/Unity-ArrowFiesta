using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviourSingleton<CameraController>
{

    [SerializeField] Transform target;
    [SerializeField] Transform cam;
    Vector3 velocity = Vector3.zero;
    public float smoothTime = .5f;
    // Start is called before the first frame update
    void Start()
    {
        if (!target)
            target = ArrowController.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);
    }
}
