using UnityEngine;
using System.Collections;

public class WheelPlatform1 : MonoBehaviour
{
    [HideInInspector]
    GameObject spotToFollow;
    int timeVar;

    // Use this for initialization
    void Start ()
    {
        // Establish point to follow on wheel
        spotToFollow = GameObject.FindGameObjectWithTag("WheelPlatSpot1");
        timeVar = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Follow point on wheel
        this.transform.position = new Vector3(spotToFollow.transform.position.x, spotToFollow.transform.position.y, this.transform.position.z);
        if (timeVar == 600) // after 6 seconds, turn 90 degrees
        {
            transform.Rotate(0, 0, -90);
        }
        if(timeVar == 700) // 1 second after turning 90 degrees, turn back
        {
            transform.Rotate(0, 0, 90);
        }
        timeVar++;
    }
}
