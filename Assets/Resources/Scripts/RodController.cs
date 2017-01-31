using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodController : MonoBehaviour
{

    private float swingSpeed = 100f;
    private float angleLimit = 45f;
    private SpriteRenderer sprite;

    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(float direction)
    {
        float newZ = transform.rotation.eulerAngles.z + (-direction * swingSpeed * Time.deltaTime);
        newZ = ClampAngle(newZ, -angleLimit, angleLimit);
        transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, newZ);
    }

    public void SwingRod()
    {
        sprite.enabled = true;
    }

    public float EndSwing()
    {
        sprite.enabled = false;
        return transform.rotation.eulerAngles.z;
    }

    private float ClampAngle(float angle, float from, float to)
    {
        if (angle > 180)
        {
            angle -= 360;
        }
        if (angle < 0)
        {
            angle = Mathf.Clamp(angle, from, 0);
        }
        else
        {
            angle = Mathf.Clamp(angle, 0, to);
        }

        return angle;
    }
}