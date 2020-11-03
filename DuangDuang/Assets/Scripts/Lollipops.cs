using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lollipops : MonoBehaviour
{
    public Transform lollipopCenter;
    public Transform pivot;
    private Vector3 rotateAxis;
    float g = 9.8f;
    float angularVelocity;
    float angularAcceleration;
    float addSpeed;

    // Start is called before the first frame update
    void Start()
    {
        addSpeed = 1.5f;
        rotateAxis = Vector3.Cross(lollipopCenter.position - pivot.position, Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        Swing();
    }

    void Swing() //Refernence: https://gameinstitute.qq.com/community/detail/110355
    {
        float r = Vector3.Distance(pivot.position, lollipopCenter.position);
        //the horizontal distance between pivot and lollipop
        Vector3 horizontal = new Vector3(pivot.position.x, lollipopCenter.position.y, pivot.position.z);
        float distance = Vector3.Distance(horizontal, lollipopCenter.position);
        //Judging the direction
        Vector3 axis = Vector3.Cross(lollipopCenter.position - pivot.position, Vector3.down);
        // normal vector
        if (Vector3.Dot(axis, rotateAxis) < 0)
        {
            distance = -distance;
        }
        float cosine = distance / r;
        //angularAcceleration = tangentialAcceleration/Radius
        angularAcceleration = g * cosine / r;
        //angularVelocity = angularVelocity + angularAcceleration*t
        angularVelocity += angularAcceleration * Time.deltaTime * addSpeed;
        //convert radian to angle
        float angle = angularVelocity * Time.deltaTime * 180.0f / Mathf.PI;
        //rotate the pivot 
        transform.RotateAround(pivot.position, rotateAxis, angle);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "TeamA" || collision.gameObject.tag == "TeamB")
        {
            Vector3 collisonPosition = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 playerPosition = new Vector3(collision.gameObject.transform.position.x, 0, collision.gameObject.transform.position.z);
            Vector3 forceDirection = playerPosition - collisonPosition;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(forceDirection * 5f, ForceMode.Impulse);
            if(collision.gameObject.GetComponent<CarController>() != null)
            {
                collision.gameObject.GetComponent<CarController>().setIsInMotionOfForce(true);
            }
            else if(collision.gameObject.GetComponent<Bot>() != null)
            {
                collision.gameObject.GetComponent<Bot>().setIsInMotionOfForce(true);
            }
            //collision.gameObject.GetComponent<Rigidbody>().AddExplosionForce(25f, lollipopCenter.position, 25f, 0, ForceMode.Impulse);
        }
    }
    
}