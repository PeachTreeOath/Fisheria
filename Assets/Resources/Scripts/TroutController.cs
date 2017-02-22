using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroutController : FishController
{

    private float moveSpeed = 2.5f;
    private float elapsedTime;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float rotAngle = Mathf.Sin(elapsedTime) / 4;
        transform.Rotate(Vector3.forward, rotAngle);
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }

    public void Spawn(Vector2 position, bool spawnOnLeft)
    {
        transform.position = position;
        if (spawnOnLeft)
        {
            transform.Rotate(Vector3.forward, 75);
        }
        else
        {
            transform.Rotate(Vector3.forward, -105);
        }
    }

}
