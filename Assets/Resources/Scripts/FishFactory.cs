using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFactory : MonoBehaviour
{

    private ResourceLoader loader;
    private float xLimit = 12;
    private float closeXLimit = 10;

    // Use this for initialization
    void Start()
    {
        loader = ResourceLoader.instance;

        Invoke("SpawnBass", 0);
        Invoke("SpawnTrout", 3);
        Invoke("SpawnOyster", 0);
        Invoke("SpawnOyster", 0);
        Invoke("SpawnShark", 3);
        Invoke("SpawnSalmon", 0);
        Invoke("SpawnPuffer", 0);
        Invoke("SpawnPuffer", 0);
        Invoke("SpawnPuffer", 0);
        Invoke("SpawnJellyfish", 0);
        Invoke("SpawnLobster", 0);
        Invoke("SpawnLobster", 0);
        Invoke("SpawnWhale", 0);
    }

    private int GetDirectionMultiplier(int direction)
    {
        if (direction == 0)
        {
            return -1;
        }
        return 1;
    }

    //TODO: Switch to coroutines
    private void SpawnBass()
    {
        CreateBass();
        Invoke("SpawnBass", 1f);
    }

    private void SpawnTrout()
    {
        CreateTrout();
        Invoke("SpawnTrout", UnityEngine.Random.Range(0, 10));
    }

    private void SpawnOyster()
    {
        CreateOyster();
        Invoke("SpawnOyster", UnityEngine.Random.Range(10, 20));
    }

    private void SpawnShark()
    {
        CreateShark();
        Invoke("SpawnShark", UnityEngine.Random.Range(10, 20));
    }

    private void SpawnSalmon()
    {
        CreateSalmon();
        Invoke("SpawnSalmon", 1f);
    }

    private void SpawnPuffer()
    {
        CreatePuffer();
    }

    private void SpawnJellyfish()
    {
        CreateJellyfish();
        Invoke("SpawnJellyfish", 30f);
    }

    private void SpawnLobster()
    {
        CreateLobster();
        Invoke("SpawnLobster", UnityEngine.Random.Range(5, 15));
    }

    private void SpawnWhale()
    {
        CreateWhale();
        Invoke("SpawnWhale", UnityEngine.Random.Range(10, 20));
    }

    private BassController CreateBass()
    {
        int choice = Random.Range(0, 20);
        float yValue = Random.Range(-4.25f, -3.25f);
        int direction = Random.Range(0, 2);

        BassController bass = (Instantiate<GameObject>(loader.bassObj)).GetComponent<BassController>();
        bass.Spawn(new Vector2(xLimit * GetDirectionMultiplier(direction), yValue), direction == 1);
        if (choice < 10)
        {
            bass.SetType(FishType.GREEN_BASS);
            bass.type = FishType.GREEN_BASS;
        }
        else if (choice < 17)
        {
            bass.SetType(FishType.BLUE_BASS);
            bass.type = FishType.BLUE_BASS;
        }
        else
        {
            bass.SetType(FishType.RED_BASS);
            bass.type = FishType.RED_BASS;
        }

        return bass;
    }

    private TroutController CreateTrout()
    {
        float xValue = Random.Range(0, 4f);
        float yValue = -4.1f;
        int direction = Random.Range(0, 2);
        int dirMult = GetDirectionMultiplier(direction);

        TroutController trout = (Instantiate<GameObject>(loader.troutObj)).GetComponent<TroutController>();
        trout.Spawn(new Vector2(xLimit * dirMult + xValue * dirMult, yValue), direction == 1);
        trout.type = FishType.TROUT;

        return trout;
    }

    private OysterController CreateOyster()
    {
        float xValue = Random.Range(-6, 6f);
        float yValue = Random.Range(-4.5f, -3.33f);

        OysterController oyster = (Instantiate<GameObject>(loader.oysterObj)).GetComponent<OysterController>();
        oyster.Spawn(new Vector2(xValue, yValue));
        oyster.type = FishType.OYSTER;

        return oyster;
    }

    private SharkController CreateShark()
    {
        float yValue = Random.Range(-1.75f, -1.25f);
        int direction = Random.Range(0, 2);

        SharkController[] sharkPack = (Instantiate<GameObject>(loader.sharkObj)).GetComponentsInChildren<SharkController>();
        foreach (SharkController shark in sharkPack)
        {
            shark.Spawn(new Vector3(xLimit * GetDirectionMultiplier(direction), yValue) + shark.transform.localPosition, direction == 1);
            if (shark.name.Equals("tigerShark"))
            {
                shark.type = FishType.TIGER_SHARK;
            }
            else
            {
                shark.type = FishType.GREAT_WHITE_SHARK;
            }
        }

        return sharkPack[0];
    }

    private SalmonController CreateSalmon()
    {
        float yValue = Random.Range(-2.75f, -0.25f);
        int direction = Random.Range(0, 2);
        float speed = UnityEngine.Random.Range(1f, 3f);

        float destYValue = Random.Range(-2.75f, -0.25f);

        SalmonController salmon = (Instantiate<GameObject>(loader.salmonObj)).GetComponent<SalmonController>();
        salmon.Spawn(new Vector2(xLimit * GetDirectionMultiplier(direction), yValue));
        salmon.SetTarget(new Vector2(-xLimit * GetDirectionMultiplier(direction), destYValue));
        salmon.SetSpeed(speed);
        salmon.type = FishType.SALMON;

        return salmon;
    }

    private PufferController CreatePuffer()
    {
        PufferController puffer = (Instantiate<GameObject>(loader.pufferObj)).GetComponent<PufferController>();
        float xValue = Random.Range(puffer.topLeftBound.x, puffer.bottomRightBound.x);
        float yValue = Random.Range(puffer.bottomRightBound.y, puffer.topLeftBound.y);
        puffer.Spawn(new Vector2(xValue, yValue));
        puffer.type = FishType.PUFFER;

        return puffer;
    }

    private JellyfishController CreateJellyfish()
    {
        JellyfishController jellyfish = (Instantiate<GameObject>(loader.jellyfishObj)).GetComponent<JellyfishController>();
        int direction = Random.Range(0, 2);
        float yValue = Random.Range(jellyfish.bottomRightBound.y, jellyfish.topLeftBound.y);
        jellyfish.Spawn(new Vector2(closeXLimit * GetDirectionMultiplier(direction), yValue));
        jellyfish.type = FishType.JELLYFISH;

        return jellyfish;
    }

    private LobsterController CreateLobster()
    {
        LobsterController lobster = (Instantiate<GameObject>(loader.lobsterObj)).GetComponent<LobsterController>();
        int direction = Random.Range(0, 2);
        float yValue = Random.Range(lobster.bottomRightBound.y, lobster.topLeftBound.y);
        lobster.Spawn(new Vector2(closeXLimit * GetDirectionMultiplier(direction), yValue));
        lobster.type = FishType.LOBSTER;

        return lobster;
    }

    private WhaleController CreateWhale()
    {
        WhaleController whale = (Instantiate<GameObject>(loader.whaleObj)).GetComponent<WhaleController>();
        int direction = Random.Range(0, 2);
        float yValue = 1.5f;
        whale.Spawn(new Vector2(xLimit * GetDirectionMultiplier(direction), yValue), direction == 1);
        whale.type = FishType.WHALE;

        return whale;
    }
}
