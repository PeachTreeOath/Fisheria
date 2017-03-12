using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishermanGear {

    public int gold = 0;
    // Range of cast
    public int rangeLevel = 1;
    // Speed of cast
    public int castSpeedLevel = 1;
    // Maneuverability of cast
    public int maneuverSpeedLevel = 1;
    // Reset speed between casts
    public int resetLevel = 1;
    // Shop history
    public bool[,] purchaseHistory;

    public FishermanGear()
    {
        purchaseHistory = new bool[4, 4];
        for (int i = 0; i < 4; i++)
        {
            purchaseHistory[i, 0] = true;
        }
    }
}
