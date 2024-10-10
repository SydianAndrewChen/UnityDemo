using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform center;

    public float rotationSpeed = 5;
    private void FixedUpdate()
    {
        transform.RotateAround(center.position, center.up, rotationSpeed * Time.deltaTime);
    }
}
