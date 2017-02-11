using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTester : MonoBehaviour {

    private ResourceLoader loader;

    // Use this for initialization
    void Start () {
        
	}

    private void TestScore()
    {
        BassController bass = (Instantiate<GameObject>(loader.bassObj)).GetComponent<BassController>();
        bass.SetType(BassController.BassType.GREEN);



        GetComponent<ScoreManager>().UpdateScores();
    }
}
