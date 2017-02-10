using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : Singleton<StatsManager>
{

    public string p1JoyMap;
    public string p2JoyMap;
    public string p3JoyMap;
    public string p4JoyMap;

    public int numRound;
    public int numPlayers;
    public FishermanGear[] playerGear;
    public FishController[] playerCatches;

    // Use this for initialization
    void Start() {
        playerGear = new FishermanGear[numPlayers];
	}

    public void ProcessCatches(FishermanController player)
    {
        int value = PointProcessor.instance.GetCatchValue(player.catchList);
        playerGear[player.playerNum].gold += value;
    }

}
