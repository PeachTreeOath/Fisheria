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
        goldText = scorePanel.transform.Find("Canvas").Find("GoldText").GetComponent<Text>();

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
        if (gold < item.cost)
        {
            //TODO: play error sound
            return;
        }

        if (item.title.Equals("Ready"))
        {
            // Save gold
            gear.gold = gold;

            SceneTransitionManager.instance.GoToGame();
//ShopExitManager.instance.Finished(playerNum);
        }
        else
        {
            if (item.title.StartsWith("Rod"))
            {
                int level = item.title[4] - '0';
                if (!gear.purchaseHistory[0, level - 1])
                {
                    if (level > gear.rangeLevel)
                    {
                        gear.rangeLevel = level;
                    }
                    gear.purchaseHistory[0, level - 1] = true;
                    UpdateGold(gold - item.cost);
                }
            }
            else if (item.title.StartsWith("Boots"))
            {
                int level = item.title[6] - '0';
                if (!gear.purchaseHistory[1, level - 1])
                {
                    if (level > gear.castSpeedLevel)
                    {
                        gear.castSpeedLevel = level;
                    }
                    gear.purchaseHistory[1, level - 1] = true;
                    UpdateGold(gold - item.cost);
                }
            }
            else if (item.title.StartsWith("Gloves"))
            {
                int level = item.title[7] - '0';
                if (!gear.purchaseHistory[2, level - 1])
                {
                    if (level > gear.maneuverSpeedLevel)
                    {
                        gear.maneuverSpeedLevel = level;
                    }
                    gear.purchaseHistory[2, level - 1] = true;
                    UpdateGold(gold - item.cost);
                }
            }
            else if (item.title.StartsWith("Hat"))
            {
                int level = item.title[4] - '0';
                if (!gear.purchaseHistory[3, level - 1])
                {
                    if (level > gear.resetLevel)
                    {
                        gear.resetLevel = level;
                    }
                    gear.purchaseHistory[3, level - 1] = true;
                    UpdateGold(gold - item.cost);
                }
            }
        }
    }

    private void UpdateGold(int newGold)
    {
        gold = newGold;
        goldText.text = "$" + gold;
        UpdateAvailableGear();
    }

    private void UpdateAvailableGear()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Transform item = transform.Find("itemBorder" + i + "" + j);

                // Put checkmark for already purchased gear
                if (gear.purchaseHistory[i, j])
                {
                    item.Find("itemCheck").GetComponent<SpriteRenderer>().enabled = true;
                    item.Find("itemCover").GetComponent<SpriteRenderer>().enabled = true;
                }

                if (gold < item.GetComponent<ItemInfo>().cost)
                {
                    item.Find("itemCover").GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }
    }
}
