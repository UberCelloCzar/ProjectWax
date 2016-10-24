using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 40f;                  // Amount of force added when the player jumps.
        [SerializeField] private float m_ContinuedJumpForce = 10f;          // Amount of force added until cap while long jumping
        [SerializeField] private int m_JumpCap = 60;                       // Cap on the total force-time of a long jump
        [SerializeField] private float m_DashSpeed = 20f;                       // Dash speed
        [SerializeField] private int m_DashTime = 20;                        // Time over which dash is applied
        [SerializeField] private float m_PropelSpeed = 50f;                       // Propel speed
        [SerializeField] private int m_DashFloatFrames = 10;                       // Number of frames at beginning of dash immune to gravity
        [SerializeField] private int m_PropelTime = 20;                        // Time over which propel is applied
        [SerializeField] private int m_DashCooldown = 120;                    // Time after dash until it is available again
        //[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character                         

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        //private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        //const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        //private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        
        private int m_JumpCycles = 0; // Total cycles spent in the air
        private int m_DashCycles = 0; // Total cycles spent dashing, and then total cycles on dash cooldown
        private float m_DashMove = 0f; // Initial direction of dash
        //private float m_DistToGround = 0f; // Distance from pivot to ground
        private float m_DefaultGravity = 1f; // Default grav scale of rigidbody
        private bool m_Burn; // Burn var with accessors
        private bool m_Dash; // Dash var with accessors
        private string m_PropelCollide = ""; // Name of colliding propellable object
        private string m_IsPropelling = ""; // Name of propelled object
        private bool m_KindleCollide = false; // Kindle var with accessor
        private bool m_KindleDash = false;
        public bool kindleFloat = false;
        public bool Burn { get { return m_Burn; } set { m_Burn = value; } }
        public bool Dash { get { return m_Dash; } set { m_Dash = value; } }
        public string PropelCollide { get { return m_PropelCollide; } set { m_PropelCollide = value; } }
        public string IsPropelling {  get { return m_IsPropelling; } }
        public bool KindleCollide {  get { return m_KindleCollide; } set { m_KindleCollide = value; } }
        public bool KindleDash { get { return m_KindleDash; } set { m_KindleDash = value; } }
        public Vector2 Velocity {  get { return m_Rigidbody2D.velocity; } }
        private static GameObject thisInstance;

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            //m_CeilingCheck = transform.Find("CeilingCheck");
            //m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            m_DefaultGravity = m_Rigidbody2D.gravityScale;
            DontDestroyOnLoad(this.gameObject);

            if (thisInstance == null)
            {
                thisInstance = this.gameObject;
            }
            else
            {
                DestroyObject(this.gameObject);
            }
        }


        private void FixedUpdate()
        {
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    m_Grounded = true;
                }
            }

            //m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            //m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }


        public void HandleInput(float move, /*bool crouch,*/ bool jump, bool burn, bool dash, char latestKey)
        {
            m_Burn = burn; // Take burn
            if (!m_Dash && m_DashCycles == 0) m_Dash = dash; // Take dash when not already dashing and not on cooldown
            else if (!m_Dash && m_DashCycles > 0) m_DashCycles--; // Or keep going on cooldown
            // If crouching, check to see if the character can stand up
            //if (!crouch && m_Anim.GetBool("Crouch"))
            //{
            //    // If the character has a ceiling preventing them from standing up, keep them crouching
            //    if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            //    {
            //        crouch = true;
            //    }
            //}

            //// Set whether or not the character is crouching in the animator
            //m_Anim.SetBool("Crouch", crouch);

            

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                //move = (crouch ? move*m_CrouchSpeed : move);

                if (m_KindleCollide && m_Dash)
                {
                    m_KindleDash = true;
                }

                if (m_PropelCollide != "" && m_Dash)
                {
                    m_IsPropelling = m_PropelCollide;
                }

                if (!m_Dash)
                {
                    // The Speed animator parameter is set to the absolute value of the horizontal input.
                    //m_Anim.SetFloat("Speed", Mathf.Abs(move));

                    if (kindleFloat == false) // if the player isn't floating
                    {
                        // Move the character
                        m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
                    }
                }
                else if (m_IsPropelling != "")
                {
                    m_Rigidbody2D.gravityScale = 0f;
                    if (m_DashCycles == 0)
                    {
                        m_DashMove = move; // Only take direction once
                    }
                    //m_Anim.SetFloat("Speed", Mathf.Abs(m_DashMove)); // Sub in propel animation here
                    this.gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;

                    m_Rigidbody2D.velocity = new Vector2(m_DashMove * m_PropelSpeed, 0); // Move according to dash rules

                    if (m_DashCycles >= m_PropelTime) // Reset if dash is over
                    {
                        m_DashCycles = m_DashCooldown; // Reuse cycle counter for cooldown
                        m_Dash = false;
                        m_IsPropelling = "";
                        m_DashMove = 0f;
                        m_Rigidbody2D.gravityScale = m_DefaultGravity;
                    }
                    else m_DashCycles++; // Or keep going if it's not

                }
                else if (m_KindleDash)
                {
                    if (m_DashCycles == 0) // Dashmove in kindle: 4=left, 1=up, 2=right, 3=down, 0=no move
                    {
                        switch(latestKey)
                        {
                            case 'a':
                                m_DashMove = 4;
                                break;
                            case 'w':
                                m_DashMove = 1;
                                break;
                            case 'd':
                                m_DashMove = 2;
                                break;
                            case 's':
                                m_DashMove = 3;
                                break;
                            default:
                                m_DashMove = 0;
                                break;
                        }
                        m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
                    }
                    //m_Anim.SetFloat("Speed", Mathf.Abs(m_DashMove)); // Sub in dash animation here
                    this.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;

                    if (m_DashCycles <= m_DashFloatFrames) // Antigrav on first frames
                    {
                        m_Rigidbody2D.gravityScale = 0f;
                    }
                    else
                    {
                        m_Rigidbody2D.gravityScale = m_DefaultGravity;
                    }

                    switch ((int)m_DashMove)
                    {
                        case 4:
                            m_Rigidbody2D.velocity = new Vector2(-1 * m_DashSpeed, m_Rigidbody2D.velocity.y);
                            break;
                        case 1:
                            m_Rigidbody2D.velocity = new Vector2(0, 1 * m_DashSpeed);
                            break;
                        case 2:
                            m_Rigidbody2D.velocity = new Vector2(1 * m_DashSpeed, m_Rigidbody2D.velocity.y);
                            break;
                        case 3:
                            m_Rigidbody2D.velocity = new Vector2(0, -1 * m_DashSpeed);
                            break;
                        default:
                            break;
                    }

                    if (m_DashCycles >= m_DashTime) // Reset if dash is over
                    {
                        m_DashCycles = m_DashCooldown; // Reuse cycle counter for cooldown
                        m_Dash = false;
                        m_KindleDash = false;
                        m_DashMove = 0f;
                    }
                    else m_DashCycles++; // Or keep going if it's not
                }
                else
                {
                    if (m_DashCycles == 0) m_DashMove = move; // Only take direction once
                    //m_Anim.SetFloat("Speed", Mathf.Abs(m_DashMove)); // Sub in dash animation here
                    this.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;

                    if (m_DashCycles <= m_DashFloatFrames)
                    {
                        m_Rigidbody2D.gravityScale = 0f;
                        m_Rigidbody2D.velocity = new Vector2(m_DashMove * m_DashSpeed, 0); // Move according to dash rules for antigrav frames
                    }
                    else
                    {
                        m_Rigidbody2D.gravityScale = m_DefaultGravity;
                        m_Rigidbody2D.velocity = new Vector2(m_DashMove * m_DashSpeed, m_Rigidbody2D.velocity.y); // Move according to dash rules for end frames
                    }

                    if (m_DashCycles >= m_DashTime) // Reset if dash is over
                    {
                        m_DashCycles = m_DashCooldown; // Reuse cycle counter for cooldown
                        m_Dash = false;
                        m_DashMove = 0f;
                    }
                    else m_DashCycles++; // Or keep going if it's not
                }

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }   
            }
            // If the player should jump...
            if (jump && m_Grounded /*&& m_Anim.GetBool("Ground")*/)
            {
                // Add a vertical force to the player.
                m_JumpCycles = 0;
                m_Grounded = false;
                //m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
            else if (jump && m_JumpCycles < m_JumpCap) // Long jump
            {
                m_Rigidbody2D.AddForce(new Vector2(0f, m_ContinuedJumpForce)); // Continue the jump
                m_JumpCycles++; // Increment counter
            }

            if (m_Burn)
            {
                // Play burn animation
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.red; // Temporary placeholders for animation
            }

            if (!m_Burn && !m_Dash)
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        public void fullStop() // Kills velocity
        {
            m_Rigidbody2D.velocity = new Vector2(0, 0);
        }

        public void resetDash() // Resets dash
        {
            if (!m_Dash) m_DashCycles = 0;
        }

        public void deactivateGravity()
        {
            m_Rigidbody2D.gravityScale = 0f;
        }

        public void reactivateGravity()
        {
            m_Rigidbody2D.gravityScale = m_DefaultGravity;
        }

        public void AddImpulseForce(Vector2 force)
        {
            m_Rigidbody2D.AddForce(force, ForceMode2D.Impulse);
        }
    }
}
