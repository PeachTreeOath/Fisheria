using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferController : FishController
{

    public Vector2 topLeftBound;
    public Vector2 bottomRightBound;

    private float moveSpeed = 0.5f;

    private Vector2 nextLocation;

    void Start()
    {
        ChooseNextLocation();
    }

    // Update is called once per frame
    void Update()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, nextLocation, step);

        if (transform.position.Equals(nextLocation))
        {
            ChooseNextLocation();
        }
    }

    public void Spawn(Vector2 position)
    {
        transform.position = position;
    }

    private void ChooseNextLocation()
    {
        float newX = UnityEngine.Random.Range(topLeftBound.x, bottomRightBound.x);
        float newY = UnityEngine.Random.Range(bottomRightBound.y, topLeftBound.y);

        nextLocation = new Vector2(newX, newY);
    }
}
