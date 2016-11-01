using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    [HideInInspector] public LevelManager levelManager;
    [SerializeField] private bool triggered = false; // Has this checkpoint been used
    Sprite litSprite;//holds the lit sprite
    // Use this for initialization
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>(); // Get the level manager
        litSprite = Resources.Load<Sprite>("Lit_Checkpoint.png");//gets the lit sprite
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.gameObject.name == "Character")
        {
            levelManager.currentCheckpoint = this.gameObject; // Make this the new checkpoint
            triggered = true;

            //this.gameObject.GetComponent<SpriteRenderer>().sprite = litSprite;//sets the lit sprite
            this.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
