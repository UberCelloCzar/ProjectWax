using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

    [HideInInspector] public LevelManager levelManager;

	// Use this for initialization
	void Start () {
        levelManager = FindObjectOfType<LevelManager>(); // Get the level manager
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Character")
        {
            levelManager.RespawnPlayer();
        }
    }
}
