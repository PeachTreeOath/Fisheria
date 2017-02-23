using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OysterController : FishController
{

    public Vector2 topLeftBound;
    public Vector2 bottomRightBound;

    public Sprite openSprite;
    public Sprite closedSprite;

    public float minOpenTime = 15;
    public float maxOpenTime = 30;

    private float nextOpen;
    private float elapsedTime;
    private bool isOpen;

    // Keep track of all players already in oyster so that when it opens, you can
    // fairly choose closest player to win
    private List<HookController> hookList;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        hookList = new List<HookController>();
        nextOpen = GetNextOpenTime();
        sprite.sprite = closedSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOpen)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > nextOpen)
            {
                isOpen = true;
                sprite.sprite = openSprite;
                CheckForHookCatch();
            }
        }
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

    private float GetNextOpenTime()
    {
        return UnityEngine.Random.Range(minOpenTime, maxOpenTime);
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        HookController hook = col.GetComponent<HookController>();
        if (hook != null)
        {
            hookList.Add(hook);
            CheckForHookCatch();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        HookController hook = col.GetComponent<HookController>();
        if (hook != null)
        {
            hookList.Remove(hook);
        }
    }

    // Check for closest hook once oyster opens (might be multiple upon opening)
    private void CheckForHookCatch()
    {
        if (isOpen)
        {
            // Case for multiple hooks
            if (hookList.Count > 1)
            {
                HookController closestHook = hookList[0];
                float closestDist = Vector2.Distance(transform.position, closestHook.transform.position);
                foreach (HookController hook in hookList)
                {
                    float dist = Vector2.Distance(transform.position, hook.transform.position);
                    if (dist < closestDist)
                    {
                        closestHook = hook;
                        closestDist = dist;
                    }
                }

                closestHook.CaughtFish(this);

                nextOpen = GetNextOpenTime();
                elapsedTime = 0;
            }
            // Case for single hook
            else if(hookList.Count == 1)
            {
                hookList[0].CaughtFish(this);

                nextOpen = GetNextOpenTime();
                elapsedTime = 0;
            }
        }
    }

}
