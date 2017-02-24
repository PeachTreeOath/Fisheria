using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Persistent global manager for transferring stats between scenes
public class StatsManager : Singleton<StatsManager>
{

    public string p1JoyMap;
    public string p2JoyMap;
    public string p3JoyMap;
    public string p4JoyMap;

    public int numRound;
    public int numPlayers;
    public FishermanGear[] playerGear;
    public Dictionary<int, List<FishController>> playerCatches;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();

        playerCatches = new Dictionary<int, List<FishController>>();
        playerGear = new FishermanGear[numPlayers];
        for (int i = 0; i < numPlayers; i++)
        {
            playerGear[i] = new FishermanGear();
        }
    }
}
