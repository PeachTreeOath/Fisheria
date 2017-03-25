using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    private float activationTime = 2;

    protected float xLimit = 12;
    protected float xLimitInbounds = 8.25f;
    protected float yLimitMin = 3.5f;
    protected float yLimitMax = 5.5f;

    protected SpriteRenderer sprite;
    protected int bossBitValue;
    protected bool activated;

    private float activatedTime;
    private SpriteRenderer bossCape;
    private SpriteRenderer bossFace;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        bossCape = transform.Find("bossCape").GetComponent<SpriteRenderer>();
        bossFace = transform.Find("bossFace").GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        if (activated)
            return;

        activatedTime += Time.deltaTime;
        if (activatedTime > activationTime)
        {
            activated = true;
        }
    }

    public void Spawn(Vector2 location)
    {
        transform.position = location;
    }

    protected void SetColor(Material mat)
    {
        bossCape.material = mat;
        bossFace.material = mat;
        transform.Find("AfterImageSystem").GetComponent<ParticleSystemRenderer>().material.SetColor("_TintColor", mat.GetColor("_Color"));
    }

    protected void ToggleSprite(bool toggle)
    {
        sprite.enabled = toggle;
        bossCape.enabled = toggle;
        bossFace.enabled = toggle;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        HookController hook = col.gameObject.GetComponent<HookController>();

        if (hook != null)
        {
            if (hook.castState == CastState.CASTING)
            {
                //TODO: Play animation
                FishermanController player = hook.GetComponentInParent<FishermanController>();
                GameManager.instance.HitBoss(player.playerNum - 1, bossBitValue);
                hook.EndCast();
            }
        }
    }
}
