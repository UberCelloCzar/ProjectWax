using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    [HideInInspector] public LevelManager levelManager;
    [SerializeField] private bool triggered = false; // Has this checkpoint been used

    // Use this for initialization
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>(); // Get the level manager
        DontDestroyOnLoad(this.gameObject);
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
        }
    }
}
