using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Calculates catch list values
public class PointProcessor : Singleton<PointProcessor>
{

    public int greenBassValue;
    public int blueBassValue;
    public int redBassValue;
    public int troutValue;
    public int troutSecondValue;
    public int tigerSharkValue;
    public int greatWhiteSharkValue;
    public int pufferValue;
    public int jellyfishValue;
    public int lobsterValue;
    public int whaleValue;

    private Dictionary<int, int> troutValues;
    private Dictionary<int, int> troutCounts;

    protected override void Awake()
    {
        base.Awake();

        troutValues = new Dictionary<int, int>();
        troutCounts = new Dictionary<int, int>();
        for (int i = 1; i < 5; i++)
        {
            troutValues[i] = 0;
        }
    }

    public void CalculateGroupPoints(Dictionary<int, List<FishController>> playerCatches)
    {
        int winningCount = 0;
        int nextWinningCount = 0;

        // Process counts
        for (int i = 1; i < 5; i++)
        {
            List<FishController> list = playerCatches[i];
            if (list == null)
                break;

            int count = 0;
            foreach (FishController fish in list)
            {
                if (fish.type == FishType.TROUT)
                {
                    count++;
                }
            }

            troutCounts[i] = count;
            if (count > winningCount)
            {
                nextWinningCount = winningCount;
                winningCount = count;
            }
            else if (count > nextWinningCount)
            {
                nextWinningCount = count;
            }
        }

        List<int> winners = new List<int>();
        List<int> secondWinners = new List<int>();

        // Process winners
        for (int i = 1; i < 5; i++)
        {
            if (winningCount != 0 && troutCounts[i] == winningCount)
            {
                winners.Add(i);
            }
            else if (nextWinningCount != 0 && troutCounts[i] == nextWinningCount)
            {
                secondWinners.Add(i);
            }
        }

        // Process values
        if (winners.Count > 0)
        {
            int winningValue = troutValue / winners.Count;
            foreach (int i in winners)
            {
                troutValues[i] = winningValue;
            }
        }
        if (secondWinners.Count > 0)
        {
            int secondWinningValue = troutSecondValue / secondWinners.Count;
            foreach (int i in secondWinners)
            {
                troutValues[i] = secondWinningValue;
            }
        }
    }

    public int GetTroutValue(int playerNum, out int count)
    {
        count = troutCounts[playerNum];
        return troutValues[playerNum];
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
            case FishType.TIGER_SHARK:
                return GetTigerSharkValue(catchList, out count);
            case FishType.GREAT_WHITE_SHARK:
                return GetGreatWhiteSharkValue(catchList, out count);
            case FishType.SALMON:
                return GetSalmonValue(catchList, out count);
            case FishType.PUFFER:
                return GetPufferValue(catchList, out count);
            case FishType.JELLYFISH:
                return GetJellyfishValue(catchList, out count);
            case FishType.LOBSTER:
                return GetLobsterValue(catchList, out count);
            case FishType.WHALE:
                return GetWhaleValue(catchList, out count);
        }

        count = 0;
        return 0;
    }

    private int GetGreenBassValue(List<FishController> catchList, out int count)
    {
        return GetStandardValue(catchList, FishType.GREEN_BASS, greenBassValue, out count);
    }

    private int GetBlueBassValue(List<FishController> catchList, out int count)
    {
        return GetStandardValue(catchList, FishType.BLUE_BASS, blueBassValue, out count);
    }

    private int GetRedBassValue(List<FishController> catchList, out int count)
    {
        return GetStandardValue(catchList, FishType.RED_BASS, redBassValue, out count);
    }

    private int GetTigerSharkValue(List<FishController> catchList, out int count)
    {
        return GetStandardValue(catchList, FishType.TIGER_SHARK, tigerSharkValue, out count);
    }

    private int GetGreatWhiteSharkValue(List<FishController> catchList, out int count)
    {
        return GetStandardValue(catchList, FishType.GREAT_WHITE_SHARK, greatWhiteSharkValue, out count);
    }

    private int GetPufferValue(List<FishController> catchList, out int count)
    {
        return GetStandardValue(catchList, FishType.PUFFER, pufferValue, out count);
    }

    private int GetWhaleValue(List<FishController> catchList, out int count)
    {
        return GetStandardValue(catchList, FishType.WHALE, whaleValue, out count);
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

    private int GetSalmonValue(List<FishController> catchList, out int count)
    {
        int total = 0;
        int totalCount = 0;

        int currentValue = 1;

        foreach (FishController fish in catchList)
        {
            if (fish.type == FishType.SALMON)
            {
                total += currentValue;
                totalCount++;
                currentValue += totalCount + 1;
            }
        }
        
        count = totalCount;
        return total;
    }

    private int GetJellyfishValue(List<FishController> catchList, out int count)
    {
        int total = 0;
        int totalCount = 0;

        foreach (FishController fish in catchList)
        {
            if (fish.type == FishType.JELLYFISH)
            {
                totalCount++;
                total = jellyfishValue;
            }
        }

        count = totalCount;
        return total;
    }

    private int GetLobsterValue(List<FishController> catchList, out int count)
    {
        int total = 0;
        int totalCount = 0;
        int isPositive = -1;

        foreach (FishController fish in catchList)
        {
            if (fish.type == FishType.LOBSTER)
            {
                total += lobsterValue;
                totalCount++;
                isPositive *= -1;
            }
        }

        count = totalCount;
        return total * isPositive;
    }
}
