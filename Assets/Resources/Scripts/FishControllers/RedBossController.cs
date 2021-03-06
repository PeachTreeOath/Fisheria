﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBossController : BossController
{

    public float moveSpeed = 1;
    public float moveDistance = 4;
    public float moveDelay = 2;

    private Vector2 prevLocation;
    private Vector2 nextLocation;
    private bool needLocation = true;
    private float elapsedTime;

    // Use this for initialization
    void Start()
    {
        bossBitValue = 1;
        SetColor(ResourceLoader.instance.redMat);
        prevLocation = transform.position;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (!activated)
            return;

        if (needLocation)
        {
            ChooseNextLocation();
        }

        elapsedTime += Time.deltaTime * moveSpeed;

        transform.position = Sinerp(prevLocation, nextLocation, elapsedTime);
        if (elapsedTime > moveDelay)
        {
            prevLocation = nextLocation;
            needLocation = true;
        }
    }

    // Sine interpolation for squid-like easing movement
    private Vector2 Sinerp(Vector2 start, Vector2 end, float value)
    {
        return Vector2.Lerp(start, end, Mathf.Sin(value * Mathf.PI * 0.25f));
    }

    private void ChooseNextLocation()
    {
        Vector2 newLoc;
        int i = 0;
        while (true)
        {
            float nextMoveDist = UnityEngine.Random.Range(moveDistance / 2, moveDistance);
            newLoc = (UnityEngine.Random.insideUnitCircle * nextMoveDist) + (Vector2)transform.position;
            if (newLoc.x > -xLimitInbounds && newLoc.x < xLimitInbounds &&
                newLoc.y < yLimitMax && newLoc.y > yLimitMin)
            {
                break;
            }
            i++;
            if (i > 100)
            {
                Debug.LogError("Controller can't find next location");
                break;
            }
        }

        if (newLoc != null)
        {
            nextLocation = newLoc;
            needLocation = false;
            elapsedTime = 0;
        }
    }
}
