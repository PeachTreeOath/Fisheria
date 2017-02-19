using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BassController : FishController
{

    private float moveSpeed;

    private SpriteRenderer sprite;

    // Use this for initialization
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }

    public void Spawn(Vector2 position, bool spawnOnLeft)
    {
        transform.position = position;
        if (spawnOnLeft)
        {
            transform.Rotate(Vector3.forward, 90);
        }
        else
        {
            transform.Rotate(Vector3.forward, -90);
        }
    }

    public void SetType(FishType newType)
    {
        switch (newType)
        {
            case FishType.GREEN_BASS:
                sprite.material = ResourceLoader.instance.greenMat;
                moveSpeed = 1f;
                break;
            case FishType.BLUE_BASS:
                sprite.material = ResourceLoader.instance.blueMat;
                moveSpeed = 1.5f;
                break;
            case FishType.RED_BASS:
                sprite.material = ResourceLoader.instance.redMat;
                moveSpeed = 2f;
                break;
            default:
                break;
        }
    }
}
