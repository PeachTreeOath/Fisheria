using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishermanController : MonoBehaviour
{

    // Stats
    public int speedLevel;
    public int rangeLevel;
    public int rodLevel;

    // State
    public int playerNum;
    public float speedBase;
    public float speedMult;
    private CastState castState;
    private HookController hook;

    public delegate void CastDelegate();
    protected CastDelegate castCallback;

    // Use this for initialization
    void Start()
    {
        hook = GetComponentInChildren<HookController>();
        castCallback = CastFinished;
        hook.CastHook(castCallback, speedLevel, rangeLevel, rodLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (castState == CastState.READY)
        {
            if (Input.GetAxis("Horizontal" + playerNum) < 0)
            {
                float newX = transform.position.x - ((speedBase + (speedLevel * speedMult)) * Time.deltaTime);
                transform.position = new Vector2(newX, transform.position.y);
            }
            else if (Input.GetAxis("Horizontal" + playerNum) > 0)
            {
                float newX = transform.position.x + ((speedBase + (speedLevel * speedMult)) * Time.deltaTime);
                transform.position = new Vector2(newX, transform.position.y);
            }

            if (Input.GetButtonDown("FireA" + playerNum))
            {
                CastRod();
            }
        }
        else if(castState == CastState.CASTING)
        {
            if (Input.GetAxis("Horizontal" + playerNum) < 0)
            {
                hook.Move(-1);
            }
            else if (Input.GetAxis("Horizontal" + playerNum) > 0)
            {
                hook.Move(1);
            }
        }
    }

    private void CastRod()
    {
        castState = CastState.CASTING;
    }

    // Callback when hook returns back to player
    public void CastFinished()
    {
        Debug.Log("TEST");
    }

}
