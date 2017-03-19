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
    public int castSpeedLevel;
    public int rangeLevel;
    public int maneuverSpeedLevel;
    public int resetLevel;
    public int debugSpeedLevel;
    public int debugRangeLevel;
    public int debugRodLevel;
    public int debugResetLevel;

    // Components
    private RodController rod;
    private HookController hook;
    public delegate void CastDelegate();
    protected CastDelegate castCallback;
    public delegate void CatchDelegate(FishController fish);
    protected CatchDelegate catchCallback;

    // Misc
    private float xLimit = 8.25f;
    private float resetSpeed;
    private float resetElapsedTime;
    private int numPearls;
    private bool pearlCaughtThisCatch;

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
                float newX = Mathf.Clamp(transform.position.x - ((speedBase + (castSpeedLevel * speedMult)) * Time.deltaTime), -xLimit, xLimit);
                transform.position = new Vector2(newX, transform.position.y);
            }
            else if (Input.GetAxis("Horizontal" + playerNum) > 0)
            {
                float newX = Mathf.Clamp(transform.position.x + ((speedBase + (castSpeedLevel * speedMult)) * Time.deltaTime), -xLimit, xLimit);
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
        else if (castState == CastState.RESETTING)
        {
            resetElapsedTime += Time.deltaTime;
            if (resetElapsedTime > resetSpeed)
            {
                castState = CastState.READY;
                // TODO Temp way to show ready
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    private void InitStats()
    {
        FishermanGear gear = StatsManager.instance.playerGear[playerNum - 1];

        castSpeedLevel = gear.castSpeedLevel;
        rangeLevel = gear.rangeLevel;
        maneuverSpeedLevel = gear.maneuverSpeedLevel;
        resetLevel = gear.resetLevel;

        if (debugSpeedLevel != 0)
        {
            castSpeedLevel = debugSpeedLevel;
        }
        if (debugRangeLevel != 0)
        {
            rangeLevel = debugRangeLevel;
        }
        if (debugRodLevel != 0)
        {
            maneuverSpeedLevel = debugRodLevel;
        }
        if (debugResetLevel != 0)
        {
            resetLevel = debugResetLevel;
        }
        resetSpeed = 4 - resetLevel * 1f;
    }

    private void AimRod()
    {
        castState = CastState.AIMING;
        rod.ShowRod(true);
    }

    private void CastRod()
    {
        castState = CastState.CASTING;
        hook.CastHook(maneuverSpeedLevel, castSpeedLevel, rangeLevel);
    }

    // Callback when hook returns back to player empty
    public void CastFinished()
    {
        castState = CastState.RESETTING;
        rod.ShowRod(false);
        resetElapsedTime = 0;
        //TODO Temp indicator of readying cast
        GetComponent<SpriteRenderer>().color = Color.black;
        if (!pearlCaughtThisCatch)
        {
            numPearls = 0;
        }
        pearlCaughtThisCatch = false;
    }

    // Callback when hook returns back to player with a fish
    public void CaughtFish(FishController fish)
    {
        // Allow players to stack pearls for huge combos
        if (fish.type == FishType.OYSTER)
        {
            numPearls++;
            pearlCaughtThisCatch = true;
        }
        else
        {
            for (int i = 0; i < numPearls + 1; i++)
            {
                catchList.Add(fish);
            }
        }

        CastFinished();
    }
}
