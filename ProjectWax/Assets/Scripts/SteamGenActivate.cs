﻿using UnityEngine;
using System.Collections;

public class SteamGenActivate : MonoBehaviour
{
    private UnityStandardAssets._2D.PlatformerCharacter2D player;
    public bool activated;
    private float burnTime;
    private bool playerCollide;
    const int ACTIVATE_TIME = 1; // takes one second to burn to activate
    public int POWER_TIME; // stays active for # of SECONDS specified in the inspector
    float poweredFor; // amount of time generator has been active

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>();
        activated = false;
        burnTime = 0;
        poweredFor = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerCollide == true) // if player colliding, check if they are burning
        {
            if (player.Burn == true && burnTime < ACTIVATE_TIME)
            {
                burnTime += Time.deltaTime;
            }
            if (burnTime >= ACTIVATE_TIME)
            {
                activated = true;
                
                if (poweredFor >= POWER_TIME) // if the generator has exceeded max power time..
                {
                    activated = false;
                    burnTime = 0; // ...reset burn time
                    poweredFor = 0;
                    //Debug.Log("Power down.");
                    this.gameObject.GetComponent<Animator>().SetBool("running", false);
                }
                else // power for days // they call me Power Man // Will Burn for Coffee
                {
                    //Debug.Log("I HAVE THE POWEEEEEER");
                    activated = true;
                    poweredFor += Time.deltaTime;
                    Debug.Log(poweredFor);
                    this.gameObject.GetComponent<Animator>().SetBool("running", true);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Character") // When character hits this object, tell update
        {
            this.playerCollide = true;
            //Debug.Log("player colliding");
        }
        else
        {
            this.playerCollide = false;
            //Debug.Log("player not here");
        }
    }
}
