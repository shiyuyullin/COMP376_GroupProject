using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "TeamA" || collision.gameObject.tag == "TeamB")
        {
            Vector3 collisonPosition = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 playerPosition = new Vector3(collision.gameObject.transform.position.x, 0, collision.gameObject.transform.position.z);
            Vector3 forceDirection = playerPosition - collisonPosition;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(forceDirection * 5f, ForceMode.Impulse);

            //collision.gameObject.GetComponent<Rigidbody>().AddExplosionForce(15f, transform.position, 15f, 0, ForceMode.Impulse);
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
