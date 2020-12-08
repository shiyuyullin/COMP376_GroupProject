using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    float rotateSpeed = 180f;
    float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.RotateAround(transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
        if(timer >= 15.0f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("colide item");

        //TODO: sounds

        if(other.gameObject.tag == "TeamA" || other.gameObject.tag == "TeamB")
        {
            Destroy(gameObject);
        }
    }

}
