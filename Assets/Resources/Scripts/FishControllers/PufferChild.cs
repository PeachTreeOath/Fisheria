using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferChild : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        GetComponentInParent<PufferController>().OnCollision(col);
    }
}
