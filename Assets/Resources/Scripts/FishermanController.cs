using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishermanController : MonoBehaviour
{

    public float speed;
    public int playerNum;
    private bool isCasting;
    


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal" + playerNum) < 0)
        {
            float newX = transform.position.x - (speed * Time.deltaTime);
            transform.position = new Vector2(newX, transform.position.y);
        }
        else if (Input.GetAxis("Horizontal" + playerNum) > 0)
        {
            float newX = transform.position.x + (speed * Time.deltaTime);
            transform.position = new Vector2(newX, transform.position.y);
        }
    }
}
