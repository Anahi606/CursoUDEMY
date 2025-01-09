using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;
    public Vector2 offset;

    private Vector3 targetPosition;

    void FixedUpdate()
    {
        if (target != null)
        {
            targetPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        }
    }
}
