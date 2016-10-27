using UnityEngine;
using System.Collections;

public class WheelPlatform2 : MonoBehaviour
{
    [HideInInspector]
    GameObject spotToFollow;
    GameObject steamGen1;
    int timeVar;
    float initialX;
    float initialY;
    Quaternion initialRot;

    // Use this for initialization
    void Start ()
    {
        // Establish point to follow
        spotToFollow = GameObject.FindGameObjectWithTag("WheelPlatSpot2");
        steamGen1 = GameObject.FindGameObjectWithTag("SteamGen1");
        timeVar = 0;
        initialX = spotToFollow.transform.position.x;
        initialY = spotToFollow.transform.position.y;
        initialRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Follow point on wheel
        this.transform.position = new Vector3(spotToFollow.transform.position.x, spotToFollow.transform.position.y, this.transform.position.z);

        if (steamGen1.GetComponent<SteamGenActivate>().activated == true)
        {
            /*
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
                //transform.Rotate(0, 0, -90);
            }
            if (timeVar == 1400)
            {
                //transform.Rotate(0, 0, 90);
            }
            */
            timeVar++;
        }
        else // reset position, rotation and timeVar
        {
            timeVar = 0;
            spotToFollow.transform.position = new Vector3(initialX, initialY, transform.position.z);
            transform.rotation = initialRot;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (steamGen1.GetComponent<SteamGenActivate>().activated == true && other.gameObject.tag == "PlatformTrigger") // When platform turn trigger hits this object
        {
            transform.Rotate(0, 0, 90);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlatformTrigger") // When platform turn trigger hits this object
        {
            transform.rotation = initialRot;
        }
    }
}
