using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    private Text scoreText;

    // Use this for initialization
    void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();

        int gold = StatsManager.instance.playerGear[0].gold;
        scoreText.text = "GOLD: " + gold;
    }

}
