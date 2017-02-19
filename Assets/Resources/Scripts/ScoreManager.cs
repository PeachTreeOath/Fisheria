using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Handles coordinating and displaying scores in the score scene
public class ScoreManager : MonoBehaviour
{

    private Text scoreText;
    private bool isRevealingScores = true; //TODO: Dont set to true
    private float revealRate = 0.5f;
    private float revealElapsedTime;

    private Queue<GameObject> blocks;

    // Use this for initialization
    void Awake()
    {
        blocks = new Queue<GameObject>();
    }

    void Update()
    {
        if (blocks.Count == 0)
        {
            isRevealingScores = false;
        }
        if (isRevealingScores)
        {
            revealElapsedTime += Time.deltaTime;
            if (revealElapsedTime > revealRate)
            {
                GameObject nextBlock = blocks.Dequeue();
                nextBlock.SetActive(true);
                revealElapsedTime = 0;
            }
        }
    }

    public void ProcessCatches(List<FishController> fishController)
    {
        int value = 0;
        int count = 0;

        value = PointProcessor.instance.GetCatchValue(fishController, FishType.GREEN_BASS, out count);
        GameObject block1 = Instantiate<GameObject>(ResourceLoader.instance.scoreBlockObj);
        block1.transform.position = new Vector2(0, 3);
        block1.GetComponent<ScoreBlock>().PopulateBlock(FishType.GREEN_BASS, count, value);

        value = PointProcessor.instance.GetCatchValue(fishController, FishType.BLUE_BASS, out count);
        GameObject block2 = Instantiate<GameObject>(ResourceLoader.instance.scoreBlockObj);
        block2.transform.position = new Vector2(0, 2);
        block2.GetComponent<ScoreBlock>().PopulateBlock(FishType.BLUE_BASS, count, value);

        value = PointProcessor.instance.GetCatchValue(fishController, FishType.RED_BASS, out count);
        GameObject block3 = Instantiate<GameObject>(ResourceLoader.instance.scoreBlockObj);
        block3.transform.position = new Vector2(0, 1);
        block3.GetComponent<ScoreBlock>().PopulateBlock(FishType.RED_BASS, count, value);

        blocks.Enqueue(block1);
        blocks.Enqueue(block2);
        blocks.Enqueue(block3);

        foreach (GameObject block in blocks)
        {
            block.SetActive(false);
        }
        //playerGear[player.playerNum - 1].gold += value;
    }

    public void UpdateScores()
    {
        int gold = StatsManager.instance.playerGear[0].gold;
        //scoreText.text = "GOLD: " + gold;
    }

}
