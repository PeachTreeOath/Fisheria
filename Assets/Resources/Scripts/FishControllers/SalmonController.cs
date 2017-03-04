using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalmonController : FishController
{

    private float moveSpeed;

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

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
