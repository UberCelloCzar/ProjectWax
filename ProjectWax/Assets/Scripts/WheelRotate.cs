using UnityEngine;
using System.Collections;

public class WheelRotate : MonoBehaviour
{
    private Vector3 rotationAxis;
    private float rotationSpeed;

	// Use this for initialization
	void Start ()
    {
        rotationSpeed = -15.5f;
        rotationAxis = Vector3.forward;
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
	}
}
