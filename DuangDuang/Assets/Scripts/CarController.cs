using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CarController : MonoBehaviour
{
    //movement
    [SerializeField] float mSpeed;
    [SerializeField] float mAngularSpeed;

    //bumper
    [SerializeField] float mForce;
    [SerializeField] float mBumperForce;
    [SerializeField] float mForceRadius;

    //Using fixed updated to get a smooth movement
    void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("aaa");
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal") * mAngularSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * mSpeed * Time.deltaTime;
        transform.Translate(vertical, 0, 0);
        transform.Rotate(0, horizontal, 0);
    }

}
