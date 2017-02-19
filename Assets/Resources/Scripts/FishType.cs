using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FishType
{

    GREEN_BASS,
    BLUE_BASS,
    RED_BASS
}

public static class FishTypeExtensions
{
    public static string GetNameString(this FishType fish)
    {
        switch (fish)
        {
            case FishType.GREEN_BASS:
                return "Green Bass";
            case FishType.BLUE_BASS:
                return "Blue Bass";
            case FishType.RED_BASS:
                return "Red Bass";
        }

        return "FAIL FISH";
    }
}