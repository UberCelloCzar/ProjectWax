using UnityEngine;
using System.Collections;

public class CameraShift : MonoBehaviour
{
    private GameObject cameraMain;
    private GameObject player;
    public UnityStandardAssets._2D.Camera2DFollow followScript;
    private bool playerCollide;
    [SerializeField] private float newCameraX;
    [SerializeField] private float newCameraY;
    [SerializeField] private float newCameraZ;

	// Use this for initialization
	void Start ()
    {
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollide = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (playerCollide == true)
        {
            switch (this.tag)
            {
                case "CamFollowX":
                    newCameraX = player.transform.position.x;
                    break;
                case "CamFollowY":
                    newCameraY = player.transform.position.y;
                    break;
                case "CamFollowZ":
                    newCameraZ = player.transform.position.z;
                    break;
                default:
                    break;
            }

            // change the camera to the new values
            cameraMain.transform.position = new Vector3(newCameraX, newCameraY, newCameraZ);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Character")
        {
            playerCollide = true;
            followScript.followOff = true; // stops the camera follow script while the player is in the camera trigger
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.name == "Character")
        {
            followScript.followOff = false;
            playerCollide = false;
        }
    }
}
