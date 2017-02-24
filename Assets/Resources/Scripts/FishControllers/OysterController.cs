using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OysterController : FishController
{

    public Sprite openSprite;
    public Sprite closedSprite;

    public float minOpenTime = 15;
    public float maxOpenTime = 30;

    private float nextOpen;
    private float elapsedTime;
    private bool isOpen;

    // When oyster is open, swap shell collider with pearl collider
    private CapsuleCollider2D closedCollider;
    private CircleCollider2D openCollider;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        closedCollider = GetComponent<CapsuleCollider2D>();
        openCollider = GetComponent<CircleCollider2D>();
        nextOpen = UnityEngine.Random.Range(minOpenTime, maxOpenTime);
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
                closedCollider.enabled = false;
                openCollider.enabled = true;
            }
        }
    }

    public void Spawn(Vector2 position)
    {
        transform.position = position;
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if(isOpen)
        {
            base.OnTriggerEnter2D(col);
        }
        else
        {
            HookController hook = col.gameObject.GetComponent<HookController>();

            if (hook != null)
            {
                if (hook.castState == CastState.CASTING)
                {
                    hook.EndCast();
                }
            }
        }
    }

}
