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

    //item
    [SerializeField] float itemDuration;
    [SerializeField] float speedChangeRatio;
    [SerializeField] float largeSizeChangeRatio;
    [SerializeField] float smallSizeChangeRatio;
    [SerializeField] float alphaChangeRatio;
    float durationTimer;


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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Items")
        {
            int random = Random.Range(0, 3);
            //int random = 2;
            Debug.Log("Effect Type: " + random);
            durationTimer += Time.deltaTime;

            switch (random)
            {
                case 0:
                    StartCoroutine(changeSpeed());
                    break;
                case 1:
                    StartCoroutine(changeSizeSmall());
                    break;                  
                case 2:
                    StartCoroutine(changeSizeLarge());
                    break;
            }

            durationTimer = 0;
        }
    }

    IEnumerator changeSpeed()
    {
        float originSpeed = mSpeed;
        mSpeed *= speedChangeRatio;
        Debug.Log("Speed Changed!");

        yield return new WaitForSeconds(itemDuration);

        mSpeed = originSpeed;
        Debug.Log("Speed Back!");
    }

    IEnumerator changeSizeSmall()
    {
        Vector3 originSize = transform.localScale;
        Vector3 tempSize = originSize * smallSizeChangeRatio;
        transform.localScale = tempSize;
        Debug.Log("Size changed!");

        yield return new WaitForSeconds(itemDuration);

        transform.localScale = originSize;
        Debug.Log("Size Back!");
    }

    IEnumerator changeSizeLarge()
    {
        Vector3 originSize = transform.localScale;
        float mass = gameObject.GetComponent<Rigidbody>().mass;
        Vector3 tempSize = originSize * largeSizeChangeRatio;
        float tempMass = mass * largeSizeChangeRatio;
        transform.localScale = tempSize;
        Debug.Log("Size = " + largeSizeChangeRatio + ", Mass = " + tempMass);

        yield return new WaitForSeconds(itemDuration);

        transform.localScale = originSize;
        gameObject.GetComponent<Rigidbody>().mass = mass;
        Debug.Log("Size Back! " + ", Mass = " + gameObject.GetComponent<Rigidbody>().mass);
    }

    IEnumerator changeTransparency()
    {
        Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
        //Material[] materials = gameObject.GetComponentInChildren<Renderer>().materials;
        Material[] materials = new Material[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            materials = renderers[i].materials;
            for(int j=0; j<materials.Length; j++)
            {
                Color newColor = materials[j].color;
                newColor.a = alphaChangeRatio;
                materials[j].color = newColor;
            }
        }
        Debug.Log("Alpha changed!");

        yield return new WaitForSeconds(itemDuration);

        for (int i = 0; i < renderers.Length; i++)
        {
            materials = renderers[i].materials;
            for (int j = 0; j < materials.Length; j++)
            {
                Color newColor = materials[j].color;
                newColor.a = 1;
                materials[j].color = newColor;
            }
        }
        Debug.Log("Alpha Back!");
    }

    //public float getDuration() { return duration; }
    //public float getSpeedChangeRatio() { return speedChangeRatio; }
    //public float getLargeSizeRatio() { return largeSizeChangeRatio; }
    //public float getSmallSizeRatio() { return smallSizeChangeRatio; }
    //public float getDurationTimer() { return durationTimer; }

}
