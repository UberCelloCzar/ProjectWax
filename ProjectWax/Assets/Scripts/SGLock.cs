using UnityEngine;
using System.Collections;

public class SGLock : MonoBehaviour
{
    GameObject steamGen1; // Note: This is the first generator that this script needs, not necessarily the first in the level.
    GameObject steamGen2;
    GameObject steamGen3;
    GameObject steamGen4;
    private bool sg1Active;
    private bool sg2Active;
    private bool sg3Active;
    private bool sg4Active;
    private float initialX;
    private float initialY;

    // Use this for initialization
    void Start()
    {
        steamGen1 = GameObject.FindGameObjectWithTag("SteamGen2"); // the first generator it needs is currently the second in the level
        steamGen2 = GameObject.FindGameObjectWithTag("SteamGen3");
        steamGen3 = GameObject.FindGameObjectWithTag("SteamGen4");
        steamGen4 = GameObject.FindGameObjectWithTag("SteamGen5");
        sg1Active = false;
        sg2Active = false;
        sg3Active = false;
        sg4Active = false;
        initialX = this.transform.position.x;
        initialY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // update active states
        sg1Active = steamGen1.GetComponent<SteamGenActivate>().activated;
        sg2Active = steamGen2.GetComponent<SteamGenActivate>().activated;
        sg3Active = steamGen3.GetComponent<SteamGenActivate>().activated;
        sg4Active = steamGen4.GetComponent<SteamGenActivate>().activated;

        if (sg1Active == true && sg2Active == true && sg3Active == true && sg4Active == true) // if every generator is active, open the door
        {
            this.transform.position = new Vector3(initialX, (initialY + 3), transform.position.z); // open says-a-me
            //Debug.Log("Open");
        }
        else
        {
            this.transform.position = new Vector3(initialX, initialY, transform.position.z); // return to closed position
            //Debug.Log("Closed");
        }
    }
}
