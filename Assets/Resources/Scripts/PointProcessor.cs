using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Calculates catch list values
public class PointProcessor : Singleton<PointProcessor>
{

    //TODO: Possibly obsolete
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

    // Returns both count by reference
    public int GetCatchValue(List<FishController> catchList, FishType type, out int count)
    {
        switch (type)
        {
            case FishType.GREEN_BASS:
                return GetGreenBassValue(catchList, out count);
            case FishType.BLUE_BASS:
                return GetBlueBassValue(catchList, out count);
            case FishType.RED_BASS:
                return GetRedBassValue(catchList, out count);
        }

        count = 0;
        return 0;
    }

    private int GetGreenBassValue(List<FishController> catchList, out int count)
    {
        return GetStandardValue(catchList, FishType.GREEN_BASS, 1, out count);
    }

    private int GetBlueBassValue(List<FishController> catchList, out int count)
    {
        return GetStandardValue(catchList, FishType.BLUE_BASS, 3, out count);
    }

    private int GetRedBassValue(List<FishController> catchList, out int count)
    {
        return GetStandardValue(catchList, FishType.RED_BASS, 10, out count);
    }

    // Used for calculating simple fish count by value
    private int GetStandardValue(List<FishController> catchList, FishType type, int value, out int count)
    {
        int total = 0;
        int totalCount = 0;

        foreach (FishController fish in catchList)
        {
            if (fish.type == type)
            {
                total += value;
                totalCount++;
            }
        }

        count = totalCount;
        return total;
    }
}
