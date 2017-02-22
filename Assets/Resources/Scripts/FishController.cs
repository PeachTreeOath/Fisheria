using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{

    protected SpriteRenderer sprite;

    public FishType type { get; set; }

    protected virtual void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
}
