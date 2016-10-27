using UnityEngine;
using System.Collections;

public class RopeBurn : MonoBehaviour {
    private bool isBurned = false;
    [HideInInspector] public bool isBurning = false;
    [HideInInspector] public UnityStandardAssets._2D.PlatformerCharacter2D charScript;
    [SerializeField] private float burnFor = 10;

    void OnTriggerEnter2D(Collider2D other) // On collision
    {
        if (other.gameObject.name == "Character" && !isBurned) // Is it the player? (can be modified later to include other burning objects for chain reactions)
        {
            if (charScript.Burn) // If yes, then burn this object
            {
                isBurning = true;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other) // On hesitate
    {
        if (other.gameObject.name == "Character" && !isBurned) // Is it the player? (can be modified later to include other burning objects for chain reactions)
        {
            if (charScript.Burn) // If yes, then burn this object
            {
                isBurning = true;
            }
        }
    }

    void Update()
    {
        if (isBurning && !isBurned)
        {
            // Play burn animation etc.
            this.transform.parent.GetComponent<SpriteRenderer>().color = Color.black;
            if (!this.transform.parent.parent.GetComponent<Rope>().isBurning)
            {
                this.transform.parent.parent.GetComponent<Rope>().isBurning = true;
                this.transform.parent.parent.GetComponent<Rope>().burnUpper = int.Parse(this.transform.parent.gameObject.name.Split('_')[1]); // Get the num of this node by name
                this.transform.parent.parent.GetComponent<Rope>().burnLower = this.transform.parent.parent.GetComponent<Rope>().burnUpper;
            }
            Destroy(this.transform.parent.gameObject, burnFor); // Burn up after a while
            isBurned = true;
        }
    }
}
