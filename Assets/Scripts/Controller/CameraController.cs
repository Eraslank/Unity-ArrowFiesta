using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] Transform target;
    Vector3 velocity = Vector3.zero;
    public float smoothTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        if (!target)
            target = ArrowController.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        var offsetPos = target.position + new Vector3(0, 1f, 0);
        var targetPos = offsetPos + target.forward * -5f;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        transform.LookAt(offsetPos, Vector3.up);
    }
}
