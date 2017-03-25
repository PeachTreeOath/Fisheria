using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBossController : BossController
{
    private float flashInterval = 2.5f;
    private float teleportInterval = 3f;
    private float teleportTime;
    private bool flashed;

    // Use this for initialization
    void Start()
    {
        bossBitValue = 8;
        SetColor(ResourceLoader.instance.yellowMat);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (!activated)
            return;

        teleportTime += Time.deltaTime;
        if (!flashed && teleportTime > flashInterval)
        {
            flashed = true;
            StartCoroutine(Flash(.1f, 1));
        }
        if (teleportTime > teleportInterval)
        {
            Teleport();
            teleportTime = 0;
            flashed = false;
        }

    }

    private void Teleport()
    {
        float newX = UnityEngine.Random.Range(-xLimitInbounds, xLimitInbounds);
        float newY = UnityEngine.Random.Range(yLimitMin, yLimitMax);
        transform.position = new Vector2(newX, newY);
    }

    private IEnumerator Flash(float speed, float duration)
    {
        float i = 0;
        bool toggle = false;

        while(i < duration)
        {
            ToggleSprite(toggle);
            toggle = !toggle;
            yield return new WaitForSeconds(speed);
            i += speed;
        }

        ToggleSprite(true);
        yield return null;
    }
}
