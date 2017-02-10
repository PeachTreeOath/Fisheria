using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishermanController : MonoBehaviour
{

    // State
    public int playerNum;
    public float speedBase;
    public float speedMult;
    public List<FishController> catchList;
    private CastState castState;

    // Stats
    private int speedLevel;
    private int rangeLevel;
    private int rodLevel;

    // Components
    private RodController rod;
    private HookController hook;
    public delegate void CastDelegate();
    protected CastDelegate castCallback;
    public delegate void CatchDelegate(FishController fish);
    protected CatchDelegate catchCallback;

    // Misc
    private float xLimit = 8.25f;

    // Use this for initialization
    void Start()
    {
        catchList = new List<FishController>();
        rod = GetComponentInChildren<RodController>();
        hook = GetComponentInChildren<HookController>();
        castCallback = CastFinished;
        catchCallback = CaughtFish;
        hook.InitCallbacks(castCallback, catchCallback);
        GameManager.instance.RegisterPlayer(this);

        InitStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (castState == CastState.READY)
        {
            if (Input.GetAxis("Horizontal" + playerNum) < 0)
            {
                float newX = Mathf.Clamp(transform.position.x - ((speedBase + (speedLevel * speedMult)) * Time.deltaTime), -xLimit, xLimit);
                transform.position = new Vector2(newX, transform.position.y);
            }
            else if (Input.GetAxis("Horizontal" + playerNum) > 0)
            {
                float newX = Mathf.Clamp(transform.position.x + ((speedBase + (speedLevel * speedMult)) * Time.deltaTime), -xLimit, xLimit);
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

    private void InitStats()
    {
        FishermanGear gear = StatsManager.instance.playerGear[playerNum-1];
        speedLevel = gear.speedLevel;
        rangeLevel = gear.rangeLevel;
        rodLevel = gear.rodLevel;
    }

    private void AimRod()
    {
        castState = CastState.AIMING;
        rod.SwingRod();
    }

    private void CastRod()
    {
        castState = CastState.CASTING;
        hook.CastHook(speedLevel, rangeLevel, rodLevel);
    }

    // Callback when hook returns back to player empty
    public void CastFinished()
    {
        castState = CastState.READY;
    }

    // Callback when hook returns back to player with a fish
    public void CaughtFish(FishController fish)
    {
        catchList.Add(fish);
        CastFinished();
    }

}
