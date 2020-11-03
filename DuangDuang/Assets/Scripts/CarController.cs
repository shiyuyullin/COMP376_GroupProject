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
    [SerializeField] float forceMagnitude;
    [SerializeField] float recoil;

    private float horizontal;
    private float vertical;
    private bool wPressed;
    private bool sPressed;

    private bool InMotionOfForce;

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            wPressed = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            sPressed = true;
        }
        
        if (InMotionOfForce)
        {
            if (gameObject.GetComponent<Rigidbody>().velocity.magnitude <= 0.1)
            {
                InMotionOfForce = false;
            }
        }
        Move();

    }

    //Using fixed updated to get a smooth movement
    void FixedUpdate()
    {
        if (!InMotionOfForce)
        {
            if (wPressed)
            {
                gameObject.GetComponent<Rigidbody>().velocity = -transform.right * mSpeed;

                wPressed = false;
            }
            if (sPressed)
            {
                gameObject.GetComponent<Rigidbody>().velocity = transform.right * mSpeed;
                sPressed = false;
            }
            transform.Rotate(0, horizontal * mAngularSpeed, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Obstacles") { }

        if (collision.gameObject.tag == "TeamA" || collision.gameObject.tag == "TeamB")
        {
            Vector3 forceDirection = collision.gameObject.transform.position - gameObject.transform.position;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
            gameObject.GetComponent<Rigidbody>().AddForce(-forceDirection * recoil, ForceMode.Impulse);
            this.InMotionOfForce = true;
        }
    }


    void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        //transform.Translate(vertical, 0, 0);
        //transform.Rotate(0, horizontal, 0);
    }

    public void setIsInMotionOfForce(bool temp)
    {
        this.InMotionOfForce = temp;
    }

    public bool getIsInMotionOfForce()
    {
        return InMotionOfForce;
    }
}
