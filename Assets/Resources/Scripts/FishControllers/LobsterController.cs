﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterController : FishController
{

    public Vector2 topLeftBound;
    public Vector2 bottomRightBound;

    private float moveSpeed = 1f;
    private Vector2 nextLocation;

    void Start()
    {
        ChooseNextLocation();
    }

    // Update is called once per frame
    void Update()
    {
        if (hooked) return;

        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, nextLocation, step);

        if (Vector2.Distance(transform.position, nextLocation) < 0.1f)
        {
            ChooseNextLocation();
        }
    }

    public void Spawn(Vector2 position)
    {
        transform.position = position;
    }

    private void ChooseNextLocation()
    {
        float newX = UnityEngine.Random.Range(topLeftBound.x, bottomRightBound.x);
        float newY = UnityEngine.Random.Range(bottomRightBound.y, topLeftBound.y);

        nextLocation = new Vector2(newX, newY);

        transform.rotation = transform.position.GetRotationAngleTowardsTarget(nextLocation);
        transform.Rotate(Vector3.forward, 90);
    }
}
