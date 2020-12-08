using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusMovement : MonoBehaviour
{
    Vector3 oldPos;
    //float i = 0;

    [SerializeField] float speed;
    //[SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    Vector3 originPosition;
    float startTime;
    bool reverse = false;
    float distance;

    // Start is called before the first frame update
    void Start()
    {
        // Mathf.PingPong
        oldPos = transform.position;

        //Lerp
        startTime = Time.time;
        originPosition = transform.position;
        distance = Vector3.Distance(originPosition, endPoint.position);
    }

    // Mathf.PingPong
    void Update()
    {
        //i += 0.1f;
        //float displacement = Mathf.PingPong(i, 10);
        //transform.position = oldPos + Vector3.right * displacement;
    }

    void FixedUpdate()
    {
        float distanceMoved = (Time.time - startTime) * speed;
        float fractionOfDistance = distanceMoved / distance;
        //transform.position = Vector3.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);

        if (reverse)
        {
            transform.position = Vector3.Lerp(endPoint.position, originPosition, fractionOfDistance);
            if (Vector3.Distance(transform.position, originPosition) < 0.001f)
            {
                reverse = false;
                startTime = Time.time;
            }
            
        }
        else
        {
            transform.position = Vector3.Lerp(originPosition, endPoint.position, fractionOfDistance);
            if (Vector3.Distance(transform.position, endPoint.position) < 0.001f)
            {
                reverse = true;
                startTime = Time.time;
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "TeamA" || collision.gameObject.tag == "TeamB")
        {
            collision.gameObject.GetComponent<Rigidbody>().AddExplosionForce(15f, transform.position, 15f, 0, ForceMode.Impulse);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<CarController>() != null)
        {
            collision.gameObject.GetComponent<CarController>().setIsInMotionOfForce(true);
        }
        else if (collision.gameObject.GetComponent<Bot>() != null)
        {
            collision.gameObject.GetComponent<Bot>().setIsInMotionOfForce(true);
        }
    }
}
