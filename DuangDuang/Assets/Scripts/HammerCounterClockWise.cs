using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerCounterClockWise : MonoBehaviour
{
    [SerializeField]
    float rotateSpeed;

    void FixedUpdate()
    {
        transform.RotateAround(transform.position, -Vector3.up, rotateSpeed);
    }
}
