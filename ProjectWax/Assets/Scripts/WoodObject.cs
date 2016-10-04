using UnityEngine;
using System.Collections;

public class WoodObject : MonoBehaviour {
    private bool isBurned = false;
    [SerializeField] private UnityStandardAssets._2D.PlatformerCharacter2D charScript;

    void OnTriggerEnter2D(Collider2D other) // On collision
    {
        if (other.gameObject.name == "Character" && !isBurned) // Is it the player? (can be modified later to include other burning objects for chain reactions)
        {
            if (charScript.Burn) // If yes, then burn this object
            {
                // Play burn animation etc.
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
                isBurned = true;
            }
        }
    }
}
