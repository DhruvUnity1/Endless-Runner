using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Vector3 Offset;
    [SerializeField] Transform target;
    public float Smoothing = 0.3f;
    Vector3 velocity = Vector3.zero;

    void Start()
    {
        Offset = this.transform.position - target.position;
    }

    void FixedUpdate()// we can use lateupdate is well
    {
        Vector3 targetPosition = target.position + Offset;
        this.transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, Smoothing);
       // transform.LookAt(target);
    }
}

