using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hook behavior is shooting out a line and returning when it either
// strikes an object or runs out of Y range value.
public class HookController : MonoBehaviour
{

    private float moveSpeed;
    private float lineSpeed;
    private float yLimit;
    private CastState castState;
    private SpriteRenderer sprite;
    private Vector2 origPos;
    private FishermanController.CastDelegate endCb; // Callback to call when cast ends

    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        origPos = transform.localPosition;
        castState = CastState.READY;
    }

    // Update is called once per frame
    void Update()
    {
        if (castState == CastState.CASTING)
        {
            transform.position += transform.up * lineSpeed * Time.deltaTime;

            if (transform.position.y > yLimit)
            {
                EndCast();
            }
        }
    }

    public void CastHook(FishermanController.CastDelegate cb, int speedLevel, int rodLevel, int rangeLevel)
    {
        sprite.enabled = true;
        SetVars(speedLevel, rodLevel, rangeLevel);
        castState = CastState.CASTING;
        endCb = cb;
    }

    private void EndCast()
    {
        sprite.enabled = false;
        transform.localPosition = origPos;
        castState = CastState.READY;
        endCb();
    }

    public void Move(float direction)
    {
        float newX = transform.position.x + (direction * moveSpeed * Time.deltaTime);
        transform.position = new Vector2(newX, transform.position.y);
    }

    private void SetVars(int speedLevel, int rodLevel, int rangeLevel)
    {
        moveSpeed = speedLevel * 0.1f;
        lineSpeed = 1 + rodLevel * 2;
        yLimit = -2 + rangeLevel * 1;
    }
}
