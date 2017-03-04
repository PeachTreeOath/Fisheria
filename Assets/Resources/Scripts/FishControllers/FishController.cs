using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{

    protected SpriteRenderer sprite;
    protected bool hooked;

    public FishType type { get; set; }

    protected virtual void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if(hooked)
        {
            return;
        }

        HookController hook = col.gameObject.GetComponent<HookController>();

        if (hook != null)
        {
            if (hook.castState == CastState.CASTING)
            {
                hooked = true;
                hook.CaughtFish(this);
            }
        }
    }
}
