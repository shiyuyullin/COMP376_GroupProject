using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 position2;
    public string horizontalOrVertical;
    public string direction;
    [SerializeField]private float speed;
    private Vector3 initialPosition;
    private Vector3 movingDirection;
    private void Start()
    {
        initialPosition = this.transform.position;
    }

    private void FixedUpdate()
    {
        if(horizontalOrVertical == "horizontal" && direction == "left")
        {
            if(transform.position.x <= position2.x)
            {
                movingDirection = Vector3.right;
            }
            if(transform.position.x >= initialPosition.x)
            {
                movingDirection = Vector3.left;
            }
            this.transform.Translate(movingDirection* Time.deltaTime * speed);
        }
        if (horizontalOrVertical == "horizontal" && direction == "right")
        {
            if (transform.position.x >= position2.x)
            {
                movingDirection = Vector3.left;
            }
            if (transform.position.x <= initialPosition.x)
            {
                movingDirection = Vector3.right;
            }
            this.transform.Translate(movingDirection * Time.deltaTime * speed);
        }
        if (horizontalOrVertical == "vertical" && direction == "up")
        {
            if (transform.position.z >= position2.z)
            {
                movingDirection = Vector3.back;
            }
            if (transform.position.z <= initialPosition.z)
            {
                movingDirection = Vector3.forward;
            }
            this.transform.Translate(movingDirection * Time.deltaTime * speed);
        }
        if (horizontalOrVertical == "vertical" && direction == "down")
        {
            if (transform.position.z <= position2.z)
            {
                movingDirection = Vector3.forward;
            }
            if (transform.position.z >= initialPosition.z)
            {
                movingDirection = Vector3.back;
            }
            this.transform.Translate(movingDirection * Time.deltaTime * speed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "TeamA" || collision.gameObject.tag == "TeamB")
        {
            collision.gameObject.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "TeamA" || collision.gameObject.tag == "TeamB")
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
