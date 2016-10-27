using UnityEngine;
using System.Collections;

public class WheelPlatform1 : MonoBehaviour
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
        // Establish point to follow on wheel
        spotToFollow = GameObject.FindGameObjectWithTag("WheelPlatSpot1");
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
            if (timeVar == 550) // after 6 seconds, turn 90 degrees
            {
                transform.Rotate(0, 0, -90);
            }
            if (timeVar == 650) // 1 second after turning 90 degrees, turn back
            {
                transform.Rotate(0, 0, 90);
            }
            */
            timeVar++;
        }
        else // reset position and timeVar
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
