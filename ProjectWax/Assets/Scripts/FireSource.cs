using UnityEngine;
using System.Collections;

public class FireSource : MonoBehaviour {
    private UnityStandardAssets._2D.PlatformerCharacter2D player;
    [SerializeField] private float pullForce = 100f; // Higher values make player home in faster
    [SerializeField] private int floatTime = 120; // Number of cycles during which player is pulled towards center and antigrav'd
    private int floatCycles = 0;
 
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>();

    }
	
	// Update is called once per frame
	void Update () {

        if (player.KindleCollide && floatCycles == Mathf.FloorToInt(floatTime / 10)) // Kill momentum once inside 
        {
            player.fullStop();
            player.Dash = false;
            player.KindleDash = false;
        }

        if (player.KindleCollide && !player.KindleDash && floatCycles <= floatTime) // Pull player toward center and levitate them
        {
            player.fullStop();
            player.deactivateGravity();
            //Vector3 forceDirection = this.transform.position - player.transform.position; // Pull player in
            //player.AddImpulseForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);
            floatCycles++;
            Debug.Log(floatCycles);
        }

        if (player.KindleCollide && floatCycles == floatTime+1)
        {
            player.reactivateGravity();
            Debug.Log("gravity restored");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Character") // When character hits this object, start kindle functions
        {
            player.fullStop();
            player.Dash = false;
            player.KindleDash = false;
            floatCycles = 0;
            player.KindleCollide = true; // Welcome to my disgusting cross-script hack because I was too lazy to make my own platformer control script
            player.transform.position = this.transform.position;
            Debug.Log("It's lit");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Character") // Turn everything off on exit
        {
            if (!player.KindleDash)
            {
                player.reactivateGravity();
                Debug.Log("gravity reset");
            }
            player.KindleCollide = false;
            Debug.Log("Get snuffed");
        }
    }
}
