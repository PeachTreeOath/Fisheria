using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBossController : BossController
{

    private float moveSpeed = 5f;

    // Use this for initialization
    void Start()
    {
        bossBitValue = 2;
        SetColor(ResourceLoader.instance.grayMat);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (!activated)
            return;

        transform.position += transform.right * moveSpeed * Time.deltaTime;
        if (transform.position.x > xLimit)
        {
            float diff = transform.position.x - xLimit * 2;
            transform.position = new Vector2(diff, UnityEngine.Random.Range(yLimitMin, yLimitMax));
        }
    }
}
