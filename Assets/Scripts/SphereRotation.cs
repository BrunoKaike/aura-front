using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRotation : MonoBehaviour
{
    public float rotationSpeed = 0.5f;

    // Update is called once per frame
    void Update()
    {
        // Rotates the sphere around its y-axis (upward axis) at a constant speed
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
    }
    
}
