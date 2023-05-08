using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    private void Update()
    {
        Vector3 desiredPosition = playerTransform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(playerTransform);
    }
}
