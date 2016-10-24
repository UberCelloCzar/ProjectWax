using UnityEngine;
using System.Collections;

public class WheelPlatform2 : MonoBehaviour
{
    [HideInInspector]
    GameObject spotToFollow;
    int timeVar;

    // Use this for initialization
    void Start ()
    {
        // Establish point to follow
        spotToFollow = GameObject.FindGameObjectWithTag("WheelPlatSpot2");
        timeVar = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Follow point on wheel
        this.transform.position = new Vector3(spotToFollow.transform.position.x, spotToFollow.transform.position.y, this.transform.position.z);
        if (timeVar == 0) // rotate to show that that is what happens when a platform reaches that point
        {
            transform.Rotate(0, 0, -90);
        }
        if (timeVar == 100) // rotate back
        {
            transform.Rotate(0, 0, 90);
        }
        if (timeVar == 1300) // full rotation around // probably won't have it do the rotation. The wheel will most likely stop here
        {
            transform.Rotate(0, 0, -90);
        }
        if (timeVar == 1400)
        {
            transform.Rotate(0, 0, 90);
        }
        timeVar++;
    }
}
