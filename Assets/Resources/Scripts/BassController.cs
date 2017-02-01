using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BassController : MonoBehaviour {

    public enum BassType
    {
        GREEN,
        BLUE,
        RED
    }

    public float moveSpeed;
    [HideInInspector]
    public BassType type;

    private SpriteRenderer sprite;

    // Use this for initialization
    void Start () {
        sprite = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetType(BassType newType)
    {
        type = newType;
        switch (newType)
        {
            case BassType.GREEN:
                sprite.material = ResourceLoader.instance.greenMat;
                break;
            case BassType.BLUE:
                sprite.material = ResourceLoader.instance.blueMat;
                break;
            case BassType.RED:
                sprite.material = ResourceLoader.instance.redMat;
                break;
            default:
                break;
        }
    }

}
