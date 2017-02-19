using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointProcessor : Singleton<PointProcessor>
{

    public int GetCatchValue(List<FishController> catchList)
    {
        int total = 0;

        foreach (FishController fish in catchList)
        {
            if (fish is BassController)
            {
               
                switch (fish.type)
                {
                    case FishType.GREEN_BASS:
                        total += 1;
                        break;
                    case FishType.BLUE_BASS:
                        total += 3;
                        break;
                    case FishType.RED_BASS:
                        total += 10;
                        break;
                }
            }
        }

        return total;
    }
}
