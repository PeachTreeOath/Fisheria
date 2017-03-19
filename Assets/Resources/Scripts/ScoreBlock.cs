using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreBlock : MonoBehaviour
{

    public Text nameText;
    public Text countText;
    public Text valueText;
    public SpriteRenderer scoreSprite;

    public void PopulateBlock(FishType type, int count, int value)
    {
        nameText.text = type.GetNameString();
        countText.text = "x" + count;
        valueText.text = "$" + value;
        if (value < 0)
        {
            transform.Find("Canvas/Value").GetComponent<Text>().color = Color.red;
        }

        SpriteRenderer icon = transform.Find("ScoreIconBg/ScoreIcon").GetComponent<SpriteRenderer>();
        switch (type)
        {
            case FishType.TROUT:
                icon.sprite = ResourceLoader.instance.troutIcon;
                break;
        }
    }
}
