using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    public float roundTime;

    private Text timeText;
    private FishermanController[] players;

    // Keep track of which bosses have been hit by which players with bitmask
    private int[] bossHits;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();

        players = new FishermanController[4];
        bossHits = new int[4];
        timeText = GameObject.Find("TimeText").GetComponent<Text>();
    }

    void Start()
    {
        StatsManager.instance.numRound++;
        AudioManager.instance.PlayMusic("daytime", 1);
        //TODO: Create nighttime round
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

    public void HitBoss(int player, int bossBitValue)
    {
        bossHits[player] ^= bossBitValue;
        if (bossHits[player] == 31)
        {
            //TODO: Real win condition
            Debug.Log("GAME OVER");
        }
    }

    public void RegisterPlayer(FishermanController player)
    {
        players[player.playerNum - 1] = player;
    }

    private void UpdateTime()
    {
        roundTime -= Time.deltaTime;
        timeText.text = (int)roundTime + "";
    }

    private void RoundOver()
    {
        foreach(FishermanController player in players)
        {
            StatsManager.instance.playerCatches[player.playerNum] = player.catchList;
        }

        //TODO: Figure out why fishes carry over to score screen in deployed version

        SceneTransitionManager.instance.GoToScore();
    }
}
