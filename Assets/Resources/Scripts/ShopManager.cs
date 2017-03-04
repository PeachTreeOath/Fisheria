using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    public int playerNum;

    private Cursor cursor;
    private Text titleText;
    private Text descText;
    private Text costText;

    // Use this for initialization
    void Start()
    {
        GameObject scorePanel = GameObject.Find("ScorePanel" + playerNum);

        cursor = scorePanel.transform.Find("Cursor/CursorImage").GetComponent<Cursor>();
        titleText = scorePanel.transform.Find("ItemCanvas").Find("Title").GetComponent<Text>();
        descText = scorePanel.transform.Find("ItemCanvas").Find("Description").GetComponent<Text>();
        costText = scorePanel.transform.Find("ItemCanvas").Find("Cost").GetComponent<Text>();

        cursor.SetManager(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateUI(ItemInfo item)
    {
        titleText.text = item.title;
        descText.text = item.desc;
        if (item.cost != 0)
        {
            costText.text = "Cost: $" + item.cost;
        }
        else
        {
            costText.text = "";
        }
    }

    public void ProcessButton(ItemInfo item)
    {
        if (item.title.Equals("Ready"))
        {
            //TODO: Check for all ready
            SceneTransitionManager.instance.GoToGame();
        }
    }
}
