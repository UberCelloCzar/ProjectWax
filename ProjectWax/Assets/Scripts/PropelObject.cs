using UnityEngine;
using System.Collections;

public class PropelObject : MonoBehaviour
{
    [HideInInspector] public UnityStandardAssets._2D.PlatformerCharacter2D charScript;
    private float m_DefaultGravity = 1f; // Default grav scale of rigidbody
    private Rigidbody2D m_Rigidbody2D;

    // Use this for initialization
    void Start () {
        charScript = GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_DefaultGravity = m_Rigidbody2D.gravityScale;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Character") // When character hits this object, turn off gravity and start propulsion
        {
            charScript.PropelCollide = this.gameObject.name; // Welcome to my disgusting cross-script hack because I was too lazy to make my own platformer control script
            //Debug.Log("Propel me daddy");
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.name == "Character") // Turn everything off on exit
        {
            charScript.PropelCollide = "";
            //Debug.Log("Stop daddy");
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if (charScript.IsPropelling == this.gameObject.name)
        {
            if (this.gameObject.layer == 0) this.gameObject.layer = 8;
            if (m_Rigidbody2D.gravityScale != 0f) m_Rigidbody2D.gravityScale = 0f;
            m_Rigidbody2D.velocity = charScript.Velocity;
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.magenta; // Temporary placeholders for animation
        }
        else
        {
            if (this.gameObject.layer != 0)
            {
                this.gameObject.layer = 0;
                m_Rigidbody2D.velocity = new Vector2(0, 0);
            }
            if (m_Rigidbody2D.gravityScale == 0f) m_Rigidbody2D.gravityScale = m_DefaultGravity;
            
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.white; // Temporary placeholders for animation
        }
	}
}
