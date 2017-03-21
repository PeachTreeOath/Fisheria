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
            case FishType.GREEN_BASS:
                icon.sprite = ResourceLoader.instance.greenBassIcon;
                break;
            case FishType.BLUE_BASS:
                icon.sprite = ResourceLoader.instance.blueBassIcon;
                break;
            case FishType.RED_BASS:
                icon.sprite = ResourceLoader.instance.redBassIcon;
                break;
            case FishType.TROUT:
                icon.sprite = ResourceLoader.instance.troutIcon;
                break;
            case FishType.OYSTER:
                icon.sprite = ResourceLoader.instance.oysterIcon;
                break;
            case FishType.TIGER_SHARK:
                icon.sprite = ResourceLoader.instance.tigerSharkIcon;
                break;
            case FishType.GREAT_WHITE_SHARK:
                icon.sprite = ResourceLoader.instance.greatWhiteSharkIcon;
                break;
            case FishType.SALMON:
                icon.sprite = ResourceLoader.instance.salmonIcon;
                break;
            case FishType.PUFFER:
                icon.sprite = ResourceLoader.instance.pufferIcon;
                break;
            case FishType.JELLYFISH:
                icon.sprite = ResourceLoader.instance.jellyfishIcon;
                break;
            case FishType.LOBSTER:
                icon.sprite = ResourceLoader.instance.lobsterIcon;
                break;
            case FishType.WHALE:
                icon.sprite = ResourceLoader.instance.whaleIcon;
                break;
        }
    }
}
