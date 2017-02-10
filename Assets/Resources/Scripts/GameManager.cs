using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    public float roundTime;

    private Text timeText;
    private FishermanController[] players;

    // Use this for initialization
    void Start()
    {
        players = new FishermanController[4];
        timeText = GameObject.Find("TimeText").GetComponent<Text>();

        StatsManager.instance.numRound++;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();

        if (roundTime < 0)
        {
            RoundOver();
        }
    }

    public void RegisterPlayer(FishermanController player)
    {
        players[player.playerNum] = player;
    }

    private void UpdateTime()
    {
        roundTime -= Time.deltaTime;
        timeText.text = (int)roundTime + "";
    }

    private void RoundOver()
    {
        for(int i = 0; i < StatsManager.instance.numPlayers; i++)
        {
            StatsManager.instance.ProcessCatches(players[i]);
        }
    }
}
