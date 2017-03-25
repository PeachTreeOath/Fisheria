using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    protected SpriteRenderer sprite;

    public FishType type { get; set; }

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        HookController hook = col.gameObject.GetComponent<HookController>();

        if (hook != null)
        {
            if (hook.castState == CastState.CASTING)
            {
                //TODO: Play animation
                hook.EndCast();
            }
        }
    }
}
