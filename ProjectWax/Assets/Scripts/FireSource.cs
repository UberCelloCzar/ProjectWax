using UnityEngine;
using System.Collections;

public class FireSource : MonoBehaviour {
    private UnityStandardAssets._2D.PlatformerCharacter2D player;
    //[SerializeField] private float pullForce = 100f; // Higher values make player home in faster
    [SerializeField] private int floatTime = 120; // Number of cycles during which player is pulled towards center and antigrav'd
    private bool playerCollide = false;
    [SerializeField] private bool lit = false;
    private int floatCycles = 0;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>();
        if (lit == true)
        {
            this.transform.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            this.transform.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (lit == true)
        {
            if (floatCycles != 0 && player.KindleCollide == false) // reset nonzero floatcycles if not kindling
            {
                floatCycles = 0;
                //Debug.Log("Float cycles reset");
            }
            if (player.KindleCollide && floatCycles == Mathf.FloorToInt(floatTime / 10)) // Kill momentum once inside 
            {
                player.fullStop();
                player.Dash = false;
                player.KindleDash = false;

            }

            if (player.KindleCollide && !player.KindleDash && floatCycles <= floatTime) // Pull player toward center and levitate them
            {
                player.kindleFloat = true;
                player.fullStop();
                player.deactivateGravity();
                //Vector3 forceDirection = this.transform.position - player.transform.position; // Pull player in
                //player.AddImpulseForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);
                floatCycles++;
                //Debug.Log(floatCycles);
            }

            if (player.KindleCollide && floatCycles == floatTime + 1)
            {
                player.reactivateGravity();
                player.kindleFloat = false;
                //Debug.Log("gravity restoredddddd");
                //Debug.Log("Max reached cycles: " + floatCycles);
            }
        }
        else
        {
            if (playerCollide == true)
            {
                //Debug.Log("Yooooo");
                if (player.Burn) // If yes, then burn this object
                {
                    lit = true;
                    this.transform.GetComponent<SpriteRenderer>().color = Color.red;
                    //Debug.Log("Let's goooo");
                    player.fullStop();
                    player.Dash = false;
                    player.KindleDash = false;
                    floatCycles = 0;
                    player.KindleCollide = true; // Welcome to my disgusting cross-script hack because I was too lazy to make my own platformer control script
                    player.transform.position = this.transform.position;
                    //Debug.Log("It's lit");
                    //Debug.Log("Enter cycles: " + floatCycles);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Character") // When character hits this object, start kindle functions
        {
            if (lit == true)
            {
                player.fullStop();
                player.Dash = false;
                player.KindleDash = false;
                floatCycles = 0;
                player.KindleCollide = true; // Welcome to my disgusting cross-script hack because I was too lazy to make my own platformer control script
                player.transform.position = this.transform.position;
                //Debug.Log("It's lit");
                //Debug.Log("Enter cycles: " + floatCycles);
            }
            else // tell update that the character is colliding
            {
                playerCollide = true;
            }
        }
        else
        {
            playerCollide = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (lit == true)
        {
            if (other.gameObject.name == "Character") // Turn everything off on exit
            {
                if (!player.KindleDash)
                {
                    player.reactivateGravity();
                    //Debug.Log("gravity reset");
                }
                player.KindleCollide = false;
                player.kindleFloat = false;
                floatCycles = 0;
                //Debug.Log("Get snuffed");
                //Debug.Log("Exit cycles: " + floatCycles);
            }
        }
    }
}
