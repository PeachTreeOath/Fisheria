using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkBossController : BossController
{
    private float moveSpeed = 2;
    private int isMovingLeft;
    private int isMovingUp;
    private float xMoveSpeed;
    private float yMoveSpeed;

    // Use this for initialization
    void Start()
    {
        bossBitValue = 16;
        SetColor(ResourceLoader.instance.pinkMat);

        float angle = UnityEngine.Random.Range(40, 50);
        xMoveSpeed = Mathf.Cos(Mathf.Deg2Rad * angle);
        yMoveSpeed = Mathf.Sin(Mathf.Deg2Rad * angle);

        GetDirectionMultiplier(UnityEngine.Random.Range(0, 2));
        isMovingLeft = GetDirectionMultiplier(UnityEngine.Random.Range(0, 2));
        isMovingUp = GetDirectionMultiplier(UnityEngine.Random.Range(0, 2));
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (!activated)
            return;

        float deltaX = moveSpeed * xMoveSpeed * Time.deltaTime * isMovingLeft;
        float deltaY = moveSpeed * yMoveSpeed * Time.deltaTime * isMovingUp;
        transform.position += new Vector3(deltaX, deltaY, 0);

        if (transform.position.x > xLimitInbounds)
            isMovingLeft = -1;
        else if (transform.position.x < -xLimitInbounds)
            isMovingLeft = 1;
        if (transform.position.y > yLimitMax)
            isMovingUp = -1;
        else if (transform.position.y < yLimitMin)
            isMovingUp = 1;
    }

    private int GetDirectionMultiplier(int direction)
    {
        if (direction == 0)
        {
            return -1;
        }
        return 1;
    }
}
