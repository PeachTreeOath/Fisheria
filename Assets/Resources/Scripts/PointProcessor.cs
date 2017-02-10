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
                BassController bass = (BassController)fish;
                switch (bass.bassType)
                {
                    case BassController.BassType.GREEN:
                        total += 1;
                        break;
                    case BassController.BassType.BLUE:
                        total += 3;
                        break;
                    case BassController.BassType.RED:
                        total += 10;
                        break;
                }
            }
        }

        return total;
    }
}
