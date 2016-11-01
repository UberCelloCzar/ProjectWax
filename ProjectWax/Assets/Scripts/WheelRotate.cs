using UnityEngine;
using System.Collections;

public class WheelRotate : MonoBehaviour
{
    private Vector3 rotationAxis;
    private float rotationSpeed;
    private float rotationAmount = 540; //the total rotation that it should rotate for
    GameObject steamGen1;

	// Use this for initialization
	void Start ()
    {
        rotationAxis = Vector3.forward;
        steamGen1 = GameObject.FindGameObjectWithTag("SteamGen1");
        rotationSpeed = (rotationAmount / steamGen1.GetComponent<SteamGenActivate>().POWER_TIME) * -1f;     //speed to rotate at based on TIME from steam generator
                                                                                                            // multiplied by -1 to rotate clockwise
    }

	// Update is called once per frame
	void Update ()
    {
        if (steamGen1.GetComponent<SteamGenActivate>().activated == true)
        { 
            transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
	}

    void Restart() // restarts and resets wheel
    {
        transform.rotation = Quaternion.Euler(0,0,0);
    }
}
