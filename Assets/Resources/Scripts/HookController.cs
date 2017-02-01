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
    private Rigidbody2D rBody;
    private Vector2 origPos;
    private FishermanController.CastDelegate endCb; // Callback to call when cast ends

    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        rBody = GetComponent<Rigidbody2D>();
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
        float angle = -direction * moveSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward, angle);
    }

    private void SetVars(int speedLevel, int rodLevel, int rangeLevel)
    {
        moveSpeed = speedLevel * 10f;
        lineSpeed = 1 + rodLevel * 2;
        yLimit = -2 + rangeLevel * 1;
    }
}
