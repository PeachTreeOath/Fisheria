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

    private Dictionary<int, int> troutValues;
    private Dictionary<int, int> troutCounts;

    protected override void Awake()
    {
        base.Awake();

        troutValues = new Dictionary<int, int>();
        troutCounts = new Dictionary<int, int>();
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
                        total += greenBassValue;
                        break;
                    case FishType.BLUE_BASS:
                        total += blueBassValue;
                        break;
                    case FishType.RED_BASS:
                        total += redBassValue;
                        break;
                }
            }
        }

        return total;
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
            case FishType.TROUT:
                count = 1;
                return 0;
            case FishType.OYSTER:
                count = 1;
                return 0;
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
