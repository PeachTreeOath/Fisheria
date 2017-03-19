using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishController : FishController
{

    public Vector2 topLeftBound;
    public Vector2 bottomRightBound;

    public float moveSpeed;
    public float moveDistance;
    public float moveDelay;

    private Vector2 prevLocation;
    private Vector2 nextLocation;
    private bool needLocation = true;
    private float elapsedTime;

    public AnimationClip clip;
    private Animation anim;

    // Use this for initialization
    void Start()
    {
        prevLocation = transform.position;
        anim = GetComponent<Animation>();
        anim.AddClip(clip, "swim");
    }

    // Update is called once per frame
    void Update()
    {
        if (hooked) return;

        if (needLocation)
        {
            ChooseNextLocation();
        }

        elapsedTime += Time.deltaTime * moveSpeed;

        transform.position = Sinerp(prevLocation, nextLocation, elapsedTime);
        if (elapsedTime > moveDelay)
        {
            prevLocation = nextLocation;
            needLocation = true;
            anim.Play("swim");
        }
    }

    // Sine interpolation for squid-like easing movement
    private Vector2 Sinerp(Vector2 start, Vector2 end, float value)
    {
        return Vector2.Lerp(start, end, Mathf.Sin(value * Mathf.PI * 0.25f));
    }

    private void ChooseNextLocation()
    {
        Vector2 newLoc;
        int i = 0;
        while (true)
        {
            newLoc = (UnityEngine.Random.insideUnitCircle * moveDistance) + (Vector2)transform.position;
            if (newLoc.x > topLeftBound.x && newLoc.x < bottomRightBound.x &&
                newLoc.y < topLeftBound.y && newLoc.y > bottomRightBound.y)
            {
                break;
            }
            i++;
            if (i > 100)
            {
                Debug.LogError("Controller can't find next location");
                break;
            }
        }

        if (newLoc != null)
        {
            nextLocation = newLoc;
            needLocation = false;
            elapsedTime = 0;
            transform.rotation = transform.position.GetRotationAngleTowardsTarget(nextLocation);
            transform.Rotate(Vector3.forward, 90);
        }
    }

    public void Spawn(Vector2 position)
    {
        transform.position = position;
    }
}
