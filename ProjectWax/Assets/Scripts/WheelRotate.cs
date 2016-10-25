using UnityEngine;
using System.Collections;

public class WheelRotate : MonoBehaviour
{
    private Vector3 rotationAxis;
    private float rotationSpeed;
    GameObject steamGen1;

	// Use this for initialization
	void Start ()
    {
        rotationSpeed = -15.5f;
        rotationAxis = Vector3.forward;
        steamGen1 = GameObject.FindGameObjectWithTag("SteamGen1");

    }

	// Update is called once per frame
	void Update ()
    {
        if (steamGen1.GetComponent<SteamGenActivate>().activated == true)
        {
            this.transform.Rotate(Vector3.zero);
            transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.zero);
        }
	}

    void Restart() // restarts and resets wheel
    {
        transform.Rotate(Vector3.zero);
    }
}
