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

        
    }

}