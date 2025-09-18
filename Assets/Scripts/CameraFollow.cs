using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Kéo player vào đây
    public float smoothSpeed = 5f;
    public Vector3 offset;

    private void LateUpdate()
    {
        if (target == null) return;
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
