using UnityEngine;
using System.Collections;

public class PropelTrigger : MonoBehaviour {

    [HideInInspector] public UnityStandardAssets._2D.PlatformerCharacter2D charScript;

    // Use this for initialization
    void Start () {
        charScript = GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Character") // When character hits this object, turn off gravity and start propulsion
        {
            charScript.PropelCollide = this.transform.parent.gameObject.name; // Welcome to my disgusting cross-script hack because I was too lazy to make my own platformer control script
            //Debug.Log("Propel me daddy");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Character") // Turn everything off on exit
        {
            charScript.PropelCollide = "";
            this.GetComponentInParent<Rigidbody2D>().velocity = new Vector2(0, 0);
            //Debug.Log("Stop daddy");
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
