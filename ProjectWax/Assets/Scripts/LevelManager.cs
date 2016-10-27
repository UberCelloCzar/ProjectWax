using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public GameObject currentCheckpoint;
    private static GameObject thisInstance;

    [SerializeField] private int numCheckpoints = 2;
    private UnityStandardAssets._2D.PlatformerCharacter2D player;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>();
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(GameObject.Find("Checkpoints"));

        if (thisInstance == null) // Don't destroy certain objects, then make sure any new instances loaded are destroyed
        {
            thisInstance = this.gameObject;
        }
        else
        {
            DestroyObject(this.gameObject);
        }

        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        for (int i = 0; i < checkpoints.Length; i++)
        {
            if (i >= numCheckpoints) Destroy(checkpoints[i]);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RespawnPlayer()
    {
        Debug.Log("Respawning...");
        player.transform.position = currentCheckpoint.transform.position; // Reset player pos
        player.fullStop();
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetSceneAt(0).name);
    }
}
