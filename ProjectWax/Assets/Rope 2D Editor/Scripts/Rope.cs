using UnityEngine;
using System.Collections.Generic;
public enum SegmentSelectionMode
{
    RoundRobin,
    Random
}
public enum LineOverflowMode
{
    Round,
    Shrink,
    Extend
}
public class Rope : MonoBehaviour {
    public SpriteRenderer[] SegmentsPrefabs;
    public SegmentSelectionMode SegmentsMode;
    public LineOverflowMode OverflowMode; 
    public bool useBendLimit = true;
    [HideInInspector]
    public int bendLimit = 45;
    [HideInInspector]
    public bool HangFirstSegment = false;
    [HideInInspector]
    public Vector2 FirstSegmentConnectionAnchor;
    [HideInInspector]
    public Vector2 LastSegmentConnectionAnchor;
    [HideInInspector]
    public bool HangLastSegment = false;
    [HideInInspector] public bool isBurning = false; // Is this rope burning?
    [HideInInspector] public int burnUpper = -1; // Origin of the burn
    [HideInInspector] public int burnLower = -1; // Both sides burn

#if UNITY_5
    [HideInInspector]
    public bool BreakableJoints=false;
    [HideInInspector]
    public float BreakForce = 10000;
#endif
    [Range(-0.5f,0.5f)]
    public float overlapFactor;
    public List<Vector3> nodes = new List<Vector3>(new Vector3[] {new Vector3(-3,0,0),new Vector3(3,0,0) });
    private RopeBurn[] childs;
    public bool WithPhysics=true;
    // Use this for initialization
    void Start () {
        childs = this.GetComponentsInChildren<RopeBurn>();
        UnityStandardAssets._2D.PlatformerCharacter2D player = GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>();
        for (int i = 0; i < childs.Length; i++)
        {
            childs[i].charScript = player; // Initialize character reference
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (isBurning && (burnLower != -2 || burnUpper != -2))
        {
            burnUpper++; // Progressively burn out from the origin
            burnLower--;
            if (burnUpper == -2) { }
            else if (burnUpper >= childs.Length)
            {
                burnUpper = -2;
            }
            else if (!childs[burnUpper].isBurning)
            {
                childs[burnUpper].isBurning = true;
            }

            if (burnLower == -2) { }
            else if (burnLower < 0)
            {
                burnLower = -2;
            }
            else if (!childs[burnLower].isBurning)
            {
                childs[burnLower].isBurning = true;
            }
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        