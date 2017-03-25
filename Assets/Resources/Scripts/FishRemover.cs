using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishRemover : MonoBehaviour {

    private float xLimit = 16;

	// Update is called once per frame
	void Update () {
	    if(transform.position.x < -xLimit || transform.position.x > xLimit)
        {
            Destroy(gameObject);
        }
	}
}
