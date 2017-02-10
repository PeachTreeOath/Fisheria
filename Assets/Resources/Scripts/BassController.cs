using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BassController : FishController
{

    public enum BassType
    {
        GREEN,
        BLUE,
        RED
    }

    private float moveSpeed;
    public BassType bassType;

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

    public void SetType(BassType newType)
    {
        bassType = newType;
        switch (newType)
        {
            case BassType.GREEN:
                sprite.material = ResourceLoader.instance.greenMat;
                moveSpeed = 1f;
                break;
            case BassType.BLUE:
                sprite.material = ResourceLoader.instance.blueMat;
                moveSpeed = 1.5f;
                break;
            case BassType.RED:
                sprite.material = ResourceLoader.instance.redMat;
                moveSpeed = 2f;
                break;
            default:
                break;
        }
    }
}
