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
    private float xLimit = 8.5f;
    private SpriteRenderer sprite;
    public CastState castState;
    public Vector2 origPos;
    public Vector2 origLocalPos;
    private FishermanController.CastDelegate castCb; // Callback to call when cast misses
    private FishermanController.CatchDelegate catchCb; // Callback to call when catch occurs
    private FishController hookedObject;
    private Vector2 vectorDiff;
    private BoxCollider2D boxCollider;

    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        boxCollider = GetComponent<BoxCollider2D>();
        origPos = transform.position;
        origLocalPos = transform.localPosition;
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

            if (transform.position.x < -xLimit || transform.position.x > xLimit)
            {
                boxCollider.enabled = false;
            }
        }
        else if (castState == CastState.REELING)
        {
            Vector2 moveVector = vectorDiff * lineSpeed * Time.deltaTime;
            transform.position += new Vector3(moveVector.x, moveVector.y, 0);
            hookedObject.transform.position = transform.position;
            if (hookedObject.transform.position.y < origPos.y)
            {
                ProcessFish();
                EndCast();
            }
        }
    }

    public void InitCallbacks(FishermanController.CastDelegate castCb, FishermanController.CatchDelegate catchCb)
    {
        this.castCb = castCb;
        this.catchCb = catchCb;
    }

    public void CastHook(int speedLevel, int rodLevel, int rangeLevel)
    {
        boxCollider.enabled = true;
        sprite.enabled = true;
        SetVars(speedLevel, rodLevel, rangeLevel);
        castState = CastState.CASTING;
        origPos = transform.position;
    }

    private void ProcessFish()
    {
        catchCb(hookedObject);
        Destroy(hookedObject.gameObject);
    }

    public void EndCast()
    {
        sprite.enabled = false;
        transform.localPosition = origLocalPos;
        castState = CastState.READY;
        castCb();
    }

    public void Move(float direction)
    {
        if (castState == CastState.CASTING)
        {
            float angle = -direction * moveSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, angle);
        }
    }

    private void SetVars(int speedLevel, int rodLevel, int rangeLevel)
    {
        moveSpeed = speedLevel * 8;
        lineSpeed = 0 + rodLevel * 1;
        yLimit = -6f + rangeLevel * 3f;
    }

    public void CaughtFish(FishController fish)
    {
        castState = CastState.REELING;
        vectorDiff = origPos - (Vector2)fish.transform.position;
        hookedObject = fish;
    }
}
