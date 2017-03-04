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

    public GameObject scoreBlockObj;

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

        scoreBlockObj = Resources.Load<GameObject>("Prefabs/ScoreBlock");
    }
}
