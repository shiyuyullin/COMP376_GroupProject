using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusMovement : MonoBehaviour
{
    float timer;
    float cycle;
    float speed;
    Rigidbody torus;
    //is kinematic = true
    private Vector3 oldPos;
    float i = 0;

    // Start is called before the first frame update
    void Start()
    {
        cycle = 2.5f;
        speed = 1f;
        torus = GetComponent<Rigidbody>();
        oldPos = transform.position;
        //is kinematic = false
        //torus.velocity = transform.right * speed;

    }

    // unity: is kinematic = true
    void FixedUpdate()
    {
        i += 0.1f;
        float displacement = Mathf.PingPong(i, 10);
        transform.position = oldPos + Vector3.right * displacement;
    }

    // unity: is kinematic = false and freeze Y position
    void Update()
    {
        //timer += Time.deltaTime;
        //if (timer > cycle)
        //{
        //    Debug.Log(transform.position.x);
        //    speed *= -1;
        //    torus.velocity = transform.right * speed;
        //    timer -= cycle;
        //}
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("collision Player");
            collision.gameObject.GetComponent<Rigidbody>().AddExplosionForce(15f, transform.position, 15f, 0, ForceMode.Impulse);
        }
    }
}
