using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{

    protected SpriteRenderer sprite;
    protected bool hooked;

    public FishType type { get; set; }

    protected Material startingMat;

    protected virtual void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        gameObject.AddComponent<FishRemover>();
        startingMat = sprite.material;
        sprite.material = ResourceLoader.instance.displacementMat;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (hooked) return;

        HookController hook = col.gameObject.GetComponent<HookController>();

        if (hook != null)
        {
            if (hook.castState == CastState.CASTING)
            {
                if (sprite != null)
                {
                    sprite.sortingLayerName = "Overwater";
                }
                else
                {
                    PufferController puffer = GetComponent<PufferController>();
                    if (puffer != null)
                    {
                        puffer.SetAboveWater();
                    }
                }
                if (startingMat != null)
                {
                    sprite.material = startingMat;
                }
                hooked = true;
                hook.CaughtFish(this);
            }
        }
    }
}
