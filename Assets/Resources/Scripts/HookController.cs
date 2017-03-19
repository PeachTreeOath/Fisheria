using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hook behavior is shooting out a line and returning when it either
// strikes an object or runs out of Y range value.
public class HookController : MonoBehaviour
{

    private float angleMoveSpeed;
    private float lineSpeed;
    private float yLimit;
    private float xLimit = 8.5f;
    private SpriteRenderer sprite;
    private SpriteRenderer lineSprite;
    public Transform lineAttach;
    public GameObject line;
    public float lineStretchSpeed;
    public CastState castState;
    public Vector2 origPos;
    public Vector2 origLocalPos;
    private FishermanController.CastDelegate castCb; // Callback to call when cast misses
    private FishermanController.CatchDelegate catchCb; // Callback to call when catch occurs
    private FishController hookedObject;
    private Vector2 vectorDiff;
    private BoxCollider2D boxCollider;
    private Quaternion origRotation;

    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        lineSprite = line.GetComponent<SpriteRenderer>();
        ShowSprites(false);
        boxCollider = GetComponent<BoxCollider2D>();
        origPos = transform.position;
        origLocalPos = transform.localPosition;
        origRotation = transform.localRotation;
        castState = CastState.READY;
    }

    // Update is called once per frame
    void Update()
    {
        if (castState == CastState.CASTING)
        {
            Stretch(line, origPos, lineAttach.position, false);
            transform.position += transform.up * lineSpeed * Time.deltaTime;

            if (transform.position.y > yLimit)
            {
                EndCast();
            }

            if (transform.position.x < -xLimit || transform.position.x > xLimit)
            {
                boxCollider.enabled = false;
            }
        }
        else if (castState == CastState.REELING)
        {
            Stretch(line, origPos, lineAttach.position, false);

            // TODO: Cap reel speed at set rate unless whale
            Vector2 moveVector = vectorDiff * lineSpeed * Time.deltaTime;
            transform.position += new Vector3(moveVector.x, moveVector.y, 0);
            hookedObject.transform.position = transform.position;
            if (hookedObject.transform.position.y < origPos.y)
            {
                ProcessFish();
            }
        }
    }

    private void Stretch(GameObject _sprite, Vector3 _initialPosition, Vector3 _finalPosition, bool _mirrorZ)
    {
        Vector2 centerPos = (_initialPosition + _finalPosition) / 2f;
        _sprite.transform.position = centerPos;
        Vector2 direction = _finalPosition - _initialPosition;
        direction = Vector3.Normalize(direction);
        _sprite.transform.right = direction;
        if (_mirrorZ) _sprite.transform.right *= -1f;
        Vector3 scale = new Vector3(1, 1, 1);
        scale.x = Vector3.Distance(_initialPosition, _finalPosition) * lineStretchSpeed;
        _sprite.transform.localScale = scale;
    }

    public void InitCallbacks(FishermanController.CastDelegate castCb, FishermanController.CatchDelegate catchCb)
    {
        this.castCb = castCb;
        this.catchCb = catchCb;
    }

    public void CastHook(int maneuverSpeedLevel, int castSpeedLevel, int rangeLevel)
    {
        boxCollider.enabled = true;
        ShowSprites(true);
        SetVars(maneuverSpeedLevel, castSpeedLevel, rangeLevel);
        castState = CastState.CASTING;
        origPos = transform.position;
        transform.localRotation = origRotation;
    }

    private void ProcessFish()
    {
        catchCb(hookedObject);
        Destroy(hookedObject.gameObject);
        ShowSprites(false);
        transform.localPosition = origLocalPos;
        castState = CastState.READY;
    }

    public void EndCast()
    {
        ShowSprites(false);
        transform.localPosition = origLocalPos;
        castState = CastState.READY;
        castCb();
    }

    public void Move(float direction)
    {
        if (castState == CastState.CASTING)
        {
            //TODO: Limit rotation to 45 deg angles
            float angle = -direction * angleMoveSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, angle);
        }
    }

    private void SetVars(int maneuverSpeedLevel, int castSpeedLevel, int rangeLevel)
    {
        angleMoveSpeed = maneuverSpeedLevel * 8;
        lineSpeed = castSpeedLevel * 0.75f;
        yLimit = -6f + rangeLevel * 3f;
    }

    public void CaughtFish(FishController fish)
    {
        castState = CastState.REELING;
        vectorDiff = origPos - (Vector2)fish.transform.position;
        hookedObject = fish;
    }

    private void ShowSprites(bool show)
    {
        sprite.enabled = show;
        lineSprite.enabled = show;
    }

}
