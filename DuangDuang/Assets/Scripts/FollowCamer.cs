using UnityEngine;

public class FollowCamer : MonoBehaviour
{
    public Transform cameraLocation;
    public float speed = 5f;
    public Vector3 distance;
    public Transform targetLocation;

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, cameraLocation.position + distance, speed * Time.deltaTime);
        transform.LookAt(targetLocation.position);

        Vector3 camToTarget = targetLocation.position - transform.position;
        Quaternion LookAtRotation = Quaternion.LookRotation(camToTarget, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, LookAtRotation, speed * Time.deltaTime);
    }

}