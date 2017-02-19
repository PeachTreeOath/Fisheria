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
        // TODO: Swap out icon
        nameText.text = type.GetNameString();
        countText.text = "x" + count;
        valueText.text = "$" + value;
    }
}
