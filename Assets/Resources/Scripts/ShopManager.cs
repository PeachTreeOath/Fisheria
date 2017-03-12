using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    public int playerNum;
    public int gold;

    private Cursor cursor;
    private Text titleText;
    private Text descText;
    private Text costText;
    private Text goldText;
    private FishermanGear gear;

    // Use this for initialization
    void Start()
    {
        GameObject scorePanel = GameObject.Find("ScorePanel" + playerNum);

        cursor = scorePanel.transform.Find("Cursor/CursorImage").GetComponent<Cursor>();
        titleText = scorePanel.transform.Find("ItemCanvas").Find("Title").GetComponent<Text>();
        descText = scorePanel.transform.Find("ItemCanvas").Find("Description").GetComponent<Text>();
        costText = scorePanel.transform.Find("ItemCanvas").Find("Cost").GetComponent<Text>();
        goldText= scorePanel.transform.Find("Canvas").Find("GoldText").GetComponent<Text>();

        cursor.SetManager(this);

        // Load gold
        gear = StatsManager.instance.playerGear[playerNum - 1];
        gold = gear.gold;
        UpdateGold(gold);
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
        if(gold < item.cost)
        {
            //TODO: play error sound
            return;
        }

        if (item.title.Equals("Ready"))
        {
            // Save gold
            gear.gold = gold;

            //TODO: Check for all ready
            SceneTransitionManager.instance.GoToGame();
        }
        else
        {
            if(item.title.StartsWith("Rod"))
            {
                gear.rangeLevel = item.title[4] - '0';
            }
            else if (item.title.StartsWith("Boots"))
            {
                gear.castSpeedLevel = item.title[6] - '0';
            }
            else if (item.title.StartsWith("Gloves"))
            {
                gear.maneuverSpeedLevel = item.title[7] - '0';
            }
            else if (item.title.StartsWith("Hat"))
            {
                gear.resetLevel = item.title[4] - '0';
            }
        }
    }

    private void UpdateGold(int newGold)
    {
        gold = newGold;
        goldText.text = "$" + gold;
    }
}
