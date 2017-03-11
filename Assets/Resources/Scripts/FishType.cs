using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FishType
{

    GREEN_BASS,
    BLUE_BASS,
    RED_BASS,
    TROUT,
    OYSTER,
    TIGER_SHARK,
    GREAT_WHITE_SHARK,
    SALMON,
    PUFFER,
    JELLYFISH,
    LOBSTER,
    WHALE,
    FISHXODIA

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
            case FishType.TROUT:
                return "Rainbow Trout";
            case FishType.OYSTER:
                return "Pearl";
            case FishType.TIGER_SHARK:
                return "Tiger Shark";
            case FishType.GREAT_WHITE_SHARK:
                return "G.White Shark";
            case FishType.SALMON:
                return "Salmon";
            case FishType.PUFFER:
                return "Puffer";
            case FishType.JELLYFISH:
                return "Jellyfish";
            case FishType.LOBSTER:
                return "Lobster";
            case FishType.WHALE:
                return "Whale";
        }

        return "FAIL FISH";
    }
}