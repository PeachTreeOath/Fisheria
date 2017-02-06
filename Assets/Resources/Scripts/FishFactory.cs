﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFactory : MonoBehaviour
{

    private ResourceLoader loader;
    private float xLimit = 12;

    // Use this for initialization
    void Start()
    {
        loader = ResourceLoader.instance;

        Invoke("SpawnBass", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private int GetDirectionMultiplier(int direction)
    {
        if (direction == 0)
        {
            return -1;
        }
        return 1;
    }

    private void SpawnBass()
    {
        CreateBass();
      //  Invoke("SpawnBass", 0.5f);
    }

    private void CreateBass()
    {
        int choice = Random.Range(0, 20);
        float yValue = Random.Range(-3.5f, -2.25f);
        int direction = Random.Range(0, 2);

        BassController bass = (Instantiate<GameObject>(loader.bassObj)).GetComponent<BassController>();
        bass.Spawn(new Vector2(xLimit * GetDirectionMultiplier(direction), yValue), direction == 1);
        if (choice < 10)
        {
            bass.SetType(BassController.BassType.GREEN);
        }
        else if (choice < 17)
        {
            bass.SetType(BassController.BassType.BLUE);
        }
        else
        {
            bass.SetType(BassController.BassType.RED);
        }
    }
}
