using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalmonController : FishController
{

    private float moveSpeed;
    private Vector2 dest;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }

    public void Spawn(Vector2 position)
    {
        transform.position = position;
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void SetTarget(Vector2 vector2)
    {
        dest = vector2;
        transform.rotation = transform.position.GetRotationAngleTowardsTarget(dest);
        transform.Rotate(Vector3.forward, 90);
    }
}
