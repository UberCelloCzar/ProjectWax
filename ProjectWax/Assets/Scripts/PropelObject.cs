using UnityEngine;
using System.Collections;

public class PropelObject : MonoBehaviour
{
    [HideInInspector] public UnityStandardAssets._2D.PlatformerCharacter2D charScript;
    [SerializeField] private float propelSpeed = 5; // Is really the mass the object is reduced to
    private float m_DefaultMass = 100f;
    private float m_DefaultGravity = 1f; // Default grav scale of rigidbody
    private Rigidbody2D m_Rigidbody2D;

    // Use this for initialization
    void Start()
    {
        charScript = GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_DefaultGravity = m_Rigidbody2D.gravityScale;
        m_DefaultMass = m_Rigidbody2D.mass;
    }
	
	// Update is called once per frame
	void Update () {
	    if (charScript.IsPropelling == this.gameObject.name)
        {
            //if (this.gameObject.layer == 0) this.gameObject.layer = 8;
            if (m_Rigidbody2D.gravityScale != 0f) m_Rigidbody2D.gravityScale = 0f;
            //m_Rigidbody2D.velocity = charScript.Velocity;
            m_Rigidbody2D.mass = propelSpeed;
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.magenta; // Temporary placeholders for animation
        }
        else
        {
            if (m_Rigidbody2D.gravityScale == 0f)
            {
                m_Rigidbody2D.velocity = new Vector2(0, 0);
                m_Rigidbody2D.gravityScale = m_DefaultGravity;
                m_Rigidbody2D.mass = m_DefaultMass;
            }
            
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.white; // Temporary placeholders for animation
        }
	}
}
