using System.Collections;
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
        for (int i = 0; i < 5; i++)
        {
            SalmonController fish = (Instantiate<GameObject>(ResourceLoader.instance.salmonObj)).GetComponent<SalmonController>();
            fish.type = FishType.SALMON;
            catchList.Add(fish);
        }
        for (int i = 0; i < 2; i++)
        {
            PufferController fish = (Instantiate<GameObject>(ResourceLoader.instance.pufferObj)).GetComponent<PufferController>();
            fish.type = FishType.PUFFER;
            catchList.Add(fish);
        }
        for (int i = 0; i < 2; i++)
        {
            JellyfishController fish = (Instantiate<GameObject>(ResourceLoader.instance.jellyfishObj)).GetComponent<JellyfishController>();
            fish.type = FishType.JELLYFISH;
            catchList.Add(fish);
        }
        for (int i = 0; i < 3; i++)
        {
            LobsterController fish = (Instantiate<GameObject>(ResourceLoader.instance.lobsterObj)).GetComponent<LobsterController>();
            fish.type = FishType.LOBSTER;
            catchList.Add(fish);
        }
        for (int i = 0; i < 2; i++)
        {
            WhaleController fish = (Instantiate<GameObject>(ResourceLoader.instance.whaleObj)).GetComponent<WhaleController>();
            fish.type = FishType.WHALE;
            catchList.Add(fish);
        }
        StatsManager.instance.playerCatches.Add(1, catchList);
        StatsManager.instance.playerCatches.Add(2, catchList);
        StatsManager.instance.playerCatches.Add(3, catchList);
        StatsManager.instance.playerCatches.Add(4, catchList);
    }
}
