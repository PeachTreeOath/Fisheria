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
    private RodController rod;
    private HookController hook;

    public delegate void CastDelegate();
    protected CastDelegate castCallback;

    // Use this for initialization
    void Start()
    {
        rod = GetComponentInChildren<RodController>();
        hook = GetComponentInChildren<HookController>();
        castCallback = CastFinished;
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
                AimRod();
            }
        }
        else if (castState == CastState.AIMING)
        {
            if (Input.GetButtonDown("FireA" + playerNum))
            {
                float rodAngle = rod.EndSwing();
                CastRod();
            }
            else
            {
                float hValue = Input.GetAxis("Horizontal" + playerNum);
                if (hValue != 0)
                {
                    rod.Move(hValue);
                }
            }
        }
        else if (castState == CastState.CASTING)
        {
            float hValue = Input.GetAxis("Horizontal" + playerNum);
            if (hValue != 0)
            {
                hook.Move(hValue);
            }
        }
    }

    private void AimRod()
    {
        castState = CastState.AIMING;
        rod.SwingRod();
    }

    private void CastRod()
    {
        castState = CastState.CASTING;
        hook.CastHook(castCallback, speedLevel, rangeLevel, rodLevel);
    }

    // Callback when hook returns back to player
    public void CastFinished()
    {
        castState = CastState.READY;
    }

}
