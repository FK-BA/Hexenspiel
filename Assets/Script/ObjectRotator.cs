using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public float rotationSpeed = 60f; // The rotation speed in degrees per second

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around its local Y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}