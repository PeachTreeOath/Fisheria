using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader : Singleton<ResourceLoader>
{
    public Material greenMat;
    public Material blueMat;
    public Material redMat;

    public GameObject bassObj;
    public GameObject troutObj;
    public GameObject oysterObj;
    public GameObject sharkObj;
    public GameObject salmonObj;
    public GameObject pufferObj;
    public GameObject jellyfishObj;
    public GameObject lobsterObj;
    public GameObject whaleObj;

    public GameObject scoreBlockObj;
    public Sprite greenBassIcon;
    public Sprite blueBassIcon;
    public Sprite redBassIcon;
    public Sprite troutIcon;
    public Sprite oysterIcon;
    public Sprite tigerSharkIcon;
    public Sprite greatWhiteSharkIcon;
    public Sprite salmonIcon;
    public Sprite pufferIcon;
    public Sprite jellyfishIcon;
    public Sprite lobsterIcon;
    public Sprite whaleIcon;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();

        greenMat = Resources.Load<Material>("Materials/GreenMat");
        blueMat = Resources.Load<Material>("Materials/BlueMat");
        redMat = Resources.Load<Material>("Materials/RedMat");

        bassObj = Resources.Load<GameObject>("Prefabs/Fish/Bass");
        troutObj = Resources.Load<GameObject>("Prefabs/Fish/Trout");
        oysterObj = Resources.Load<GameObject>("Prefabs/Fish/Oyster");
        sharkObj = Resources.Load<GameObject>("Prefabs/Fish/Shark");
        salmonObj = Resources.Load<GameObject>("Prefabs/Fish/Salmon");
        pufferObj = Resources.Load<GameObject>("Prefabs/Fish/Puffer");
        jellyfishObj = Resources.Load<GameObject>("Prefabs/Fish/Jellyfish");
        lobsterObj = Resources.Load<GameObject>("Prefabs/Fish/Lobster");
        whaleObj = Resources.Load<GameObject>("Prefabs/Fish/Whale");

        scoreBlockObj = Resources.Load<GameObject>("Prefabs/ScoreBlock");
        greenBassIcon = Resources.Load<Sprite>("Sprites/Fish/greenBassIcon");
        blueBassIcon = Resources.Load<Sprite>("Sprites/Fish/blueBassIcon");
        redBassIcon = Resources.Load<Sprite>("Sprites/Fish/redBassIcon");
        troutIcon = Resources.Load<Sprite>("Sprites/Fish/troutIcon");
        oysterIcon = Resources.Load<Sprite>("Sprites/Fish/oysterIcon");
        tigerSharkIcon = Resources.Load<Sprite>("Sprites/Fish/tigerSharkIcon");
        greatWhiteSharkIcon = Resources.Load<Sprite>("Sprites/Fish/greatWhiteSharkIcon");
        salmonIcon = Resources.Load<Sprite>("Sprites/Fish/salmonIcon");
        pufferIcon = Resources.Load<Sprite>("Sprites/Fish/pufferIcon");
        jellyfishIcon = Resources.Load<Sprite>("Sprites/Fish/jellyfishIcon");
        lobsterIcon = Resources.Load<Sprite>("Sprites/Fish/lobsterIcon");
        whaleIcon = Resources.Load<Sprite>("Sprites/Fish/whaleIcon");
    }
}
