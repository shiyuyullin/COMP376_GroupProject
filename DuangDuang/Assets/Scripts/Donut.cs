using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donut : MonoBehaviour
{

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "TeamA" || collision.gameObject.tag == "TeamB")
        {
            collision.gameObject.GetComponent<Rigidbody>().AddExplosionForce(20f, transform.position, 15f, 0, ForceMode.Impulse);
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
}
