using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBossController : BossController {

    private float moveSpeed = 2f;

    // Use this for initialization
    void Start () {
        bossBitValue = 4;
        SetColor(ResourceLoader.instance.blueMat);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (!activated)
            return;

        float newX = transform.position.x - moveSpeed * Time.deltaTime;
        float newY = 4.5f + Mathf.Sin(Time.time);
        transform.position = new Vector3(newX, newY, 0);
        if (transform.position.x < -xLimit)
        {
            float diff = transform.position.x + xLimit * 2;
            transform.position = new Vector2(diff, transform.position.y);
        }

    }
}
