using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //float rotateSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.RotateAround(transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
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
