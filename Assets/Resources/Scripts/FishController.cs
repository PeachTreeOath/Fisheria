using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FishController : MonoBehaviour {

    // Called when fished is reeled in. Add itself to owner's inventory
    abstract public void AddToInventory();
}
