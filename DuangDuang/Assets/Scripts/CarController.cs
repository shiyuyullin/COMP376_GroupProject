using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CarController : MonoBehaviour
{
    //movement
    [SerializeField] float mSpeed;
    [SerializeField] float mAngularSpeed;

    //bumper
    [SerializeField] float mForce;
    [SerializeField] float mBumperForce;
    [SerializeField] float mForceRadius;

    void Start()
    {
        mSpeed = 10f;
        mAngularSpeed = 100f;
    }

    void Update()
    {
        Move();
    }

    //private void OnCollisionEnter(Collision collisionInfo)
    //{
    //    if (collisionInfo.collider.CompareTag("Dirt"))
    //        Destroy(collisionInfo.collider.gameObject);
    //    if (collisionInfo.collider.CompareTag("Player"))
    //        collisionInfo.collider.attachedRigidbody.AddForce(mForce * Vector3.forward, ForceMode.Impulse);
    //    //collisionInfo.rigidbody.AddExplosionForce(mBumperForce, transform.position, mForceRadius, 0.0f, ForceMode.Impulse);
    //}

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal") * mAngularSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * mSpeed * Time.deltaTime;

        transform.Translate(vertical, 0, 0);
        transform.Rotate(0, horizontal, 0);
    }
}
