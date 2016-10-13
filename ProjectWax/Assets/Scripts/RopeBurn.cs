using UnityEngine;
using System.Collections;

public class RopeBurn : MonoBehaviour {
    private bool isBurned = false;
    [HideInInspector] public bool isBurning = false;
    [SerializeField] private UnityStandardAssets._2D.PlatformerCharacter2D charScript;
    [SerializeField] private float burnFor = 10;

    void OnCollisionEnter2D(Collision2D other) // On collision
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
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            if (!this.GetComponentInParent<Rope>().isBurning)
            {
                this.GetComponentInParent<Rope>().isBurning = true;
                this.GetComponentInParent<Rope>().burnUpper = int.Parse(this.gameObject.name.Split('_')[1]); // Get the num of this node by name
                this.GetComponentInParent<Rope>().burnLower = this.GetComponentInParent<Rope>().burnUpper;
            }
            Destroy(this.gameObject, burnFor); // Burn up after a while
            isBurned = true;
        }
    }
}
