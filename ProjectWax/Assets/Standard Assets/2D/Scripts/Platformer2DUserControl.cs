using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        private bool m_Burn;
        private bool m_Dash;
        private char beforePriorPreviousLatestKey; // Nice
        private char priorPreviousLatestKey;
        private char previousLatestKey;
        private char latestKey;



        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButton("Jump");
            }
            if (!m_Burn) // Read burn input for no missed presses
            {
                m_Burn = CrossPlatformInputManager.GetButton("Fire1"); // Change to get down if on press is desired rather than continuous
            }
            if (!m_Dash)
            {
                m_Dash = CrossPlatformInputManager.GetButton("Fire2"); // Get single press of dash
            }

            if (CrossPlatformInputManager.GetButtonDown("Horizontal") && CrossPlatformInputManager.GetAxisRaw("Horizontal") > 0) // D, this is still cross platform, just using key chars for clarity
            {
                beforePriorPreviousLatestKey = priorPreviousLatestKey;
                priorPreviousLatestKey = previousLatestKey;
                previousLatestKey = latestKey;
                latestKey = 'd';
            }
            if (CrossPlatformInputManager.GetButtonDown("Horizontal") && CrossPlatformInputManager.GetAxisRaw("Horizontal") < 0) // A, set the latest key
            {
                beforePriorPreviousLatestKey = priorPreviousLatestKey;
                priorPreviousLatestKey = previousLatestKey;
                previousLatestKey = latestKey;
                latestKey = 'a';
            }
            if (CrossPlatformInputManager.GetButtonDown("Vertical") && CrossPlatformInputManager.GetAxisRaw("Vertical") > 0) // W
            {
                beforePriorPreviousLatestKey = priorPreviousLatestKey;
                priorPreviousLatestKey = previousLatestKey;
                previousLatestKey = latestKey;
                latestKey = 'w';
            }
            if (CrossPlatformInputManager.GetButtonDown("Vertical") && CrossPlatformInputManager.GetAxisRaw("Vertical") < 0) // S
            {
                beforePriorPreviousLatestKey = priorPreviousLatestKey;
                priorPreviousLatestKey = previousLatestKey;
                previousLatestKey = latestKey;
                latestKey = 's';
            }

            checkIfLatest(); // Check if latestKey is valid
            //Debug.Log(latestKey.ToString());

        }


        private void FixedUpdate()
        {
            // Read the inputs.
            //bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            m_Character.HandleInput(h, /*crouch,*/ m_Jump, m_Burn, m_Dash, latestKey);
            m_Burn = false;
            m_Jump = false;
            m_Dash = false;
        }

        private void checkIfLatest() // Runs until the latest key is either nothing or is being pressed
        {
            if (latestKey == 'd' && !(CrossPlatformInputManager.GetButton("Horizontal") && CrossPlatformInputManager.GetAxisRaw("Horizontal") > 0)) // D, this is still cross platform, just using key chars for clarity
            {
                latestKey = previousLatestKey;
                previousLatestKey = priorPreviousLatestKey;
                priorPreviousLatestKey = beforePriorPreviousLatestKey;
                beforePriorPreviousLatestKey = '0';
                checkIfLatest();
            }
            if (latestKey == 'a' && !(CrossPlatformInputManager.GetButton("Horizontal") && CrossPlatformInputManager.GetAxisRaw("Horizontal") < 0)) // A, set the latest key
            {
                latestKey = previousLatestKey;
                previousLatestKey = priorPreviousLatestKey;
                priorPreviousLatestKey = beforePriorPreviousLatestKey;
                beforePriorPreviousLatestKey = '0';
                checkIfLatest();
            }
            if (latestKey == 'w' && !(CrossPlatformInputManager.GetButton("Vertical") && CrossPlatformInputManager.GetAxisRaw("Vertical") > 0)) // W
            {
                latestKey = previousLatestKey;
                previousLatestKey = priorPreviousLatestKey;
                priorPreviousLatestKey = beforePriorPreviousLatestKey;
                beforePriorPreviousLatestKey = '0';
                checkIfLatest();
            }
            if (latestKey == 's' && !(CrossPlatformInputManager.GetButton("Vertical") && CrossPlatformInputManager.GetAxisRaw("Vertical") < 0)) // S
            {
                latestKey = previousLatestKey;
                previousLatestKey = priorPreviousLatestKey;
                priorPreviousLatestKey = beforePriorPreviousLatestKey;
                beforePriorPreviousLatestKey = '0';
                checkIfLatest();
            }
        }
    }
}
