﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBlock : MonoBehaviour
{

    private Text nameText;
    private Text countText;
    private Text valueText;
    private SpriteRenderer scoreSprite;

    // Use this for initialization
    void Start()
    {
        nameText = transform.Find("Name").GetComponent<Text>();
        countText = transform.Find("Count").GetComponent<Text>();
        valueText = transform.Find("Value").GetComponent<Text>();
        scoreSprite= transform.Find("ScoreIcon").GetComponent<SpriteRenderer>();
    }

    public void Init(FishType type)
    {

    }
}
