﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTester : MonoBehaviour
{

    public bool isTestingOn;

    public void RunTests()
    {
        if (isTestingOn)
        {
            TestScore();
        }
    }

    private void TestScore()
    {
        List<FishController> catchList = new List<FishController>();

        BassController gBass1 = (Instantiate<GameObject>(ResourceLoader.instance.bassObj)).GetComponent<BassController>();
        gBass1.type = FishType.GREEN_BASS;
        catchList.Add(gBass1);
        BassController gBass2 = (Instantiate<GameObject>(ResourceLoader.instance.bassObj)).GetComponent<BassController>();
        gBass2.type = FishType.GREEN_BASS;
        catchList.Add(gBass2);
        BassController bBass = (Instantiate<GameObject>(ResourceLoader.instance.bassObj)).GetComponent<BassController>();
        bBass.type = FishType.BLUE_BASS;
        catchList.Add(bBass);
        BassController rBass = (Instantiate<GameObject>(ResourceLoader.instance.bassObj)).GetComponent<BassController>();
        rBass.type = FishType.RED_BASS;
        catchList.Add(rBass);

        StatsManager.instance.playerCatches.Add(1, catchList);
        StatsManager.instance.playerCatches.Add(2, catchList);
        StatsManager.instance.playerCatches.Add(3, catchList);
        StatsManager.instance.playerCatches.Add(4, catchList);
    }
}
