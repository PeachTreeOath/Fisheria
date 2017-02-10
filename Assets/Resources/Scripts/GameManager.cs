using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    private Text timeText;
    public float roundTime;


    // Use this for initialization
    void Start()
    {
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

    private void UpdateTime()
    {
        roundTime -= Time.deltaTime;
        timeText.text = (int)roundTime + "";
    }

    private void RoundOver()
    {

    }
}
