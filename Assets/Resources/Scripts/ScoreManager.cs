using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Handles coordinating and displaying scores in the score scene
public class ScoreManager : MonoBehaviour
{

    private bool isRevealingScores = true; //TODO: Dont set to true
    private float revealRate = 0.5f;
    private float revealElapsedTime;
    private float startingYPosition = 3;
    private float yPositionChange = 0.6f;

    private List<Queue<GameObject>> blocks;

    // Use this for initialization
    void Awake()
    {
        blocks = new List<Queue<GameObject>>();
        for (int i = 0; i < 4; i++)
        {
            blocks.Add(new Queue<GameObject>());
        }
    }

    void Start()
    {
        PointProcessor.instance.CalculateGroupPoints(StatsManager.instance.playerCatches);

        for (int i = 1; i < StatsManager.instance.numPlayers + 1; i++)
        {
            ProcessCatches(StatsManager.instance.playerCatches[i], i);
        }

        foreach (Queue<GameObject> queue in blocks)
        {
            foreach (GameObject block in queue)
            {
                block.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (blocks[0].Count == 0)
        {
            isRevealingScores = false;
        }
        if (isRevealingScores)
        {
            revealElapsedTime += Time.deltaTime;
            if (revealElapsedTime > revealRate)
            {
                foreach (Queue<GameObject> queue in blocks)
                {
                    GameObject nextBlock = queue.Dequeue();
                    if (nextBlock != null)
                    {
                        nextBlock.SetActive(true);
                    }
                    revealElapsedTime = 0;
                }
            }
        }
        else
        {
            for (int i = 1; i < 5; i++)
            {
                if (Input.GetButtonDown("FireA" + i) || Input.GetButtonDown("FireB" + i))
                {
                    SceneTransitionManager.instance.GoToGame();
                }
            }
        }
    }

    public void ProcessCatches(List<FishController> fishController, int playerNum)
    {
        int value = 0;
        int count = 0;
        float yPosition = startingYPosition;
        float xPosition = -10 + 4 * playerNum;

        value = PointProcessor.instance.GetCatchValue(fishController, FishType.GREEN_BASS, out count);
        GameObject block1 = Instantiate<GameObject>(ResourceLoader.instance.scoreBlockObj);
        block1.transform.position = new Vector2(xPosition, yPosition);
        block1.GetComponent<ScoreBlock>().PopulateBlock(FishType.GREEN_BASS, count, value);
        yPosition -= yPositionChange;

        value = PointProcessor.instance.GetCatchValue(fishController, FishType.BLUE_BASS, out count);
        GameObject block2 = Instantiate<GameObject>(ResourceLoader.instance.scoreBlockObj);
        block2.transform.position = new Vector2(xPosition, yPosition);
        block2.GetComponent<ScoreBlock>().PopulateBlock(FishType.BLUE_BASS, count, value);
        yPosition -= yPositionChange;

        value = PointProcessor.instance.GetCatchValue(fishController, FishType.RED_BASS, out count);
        GameObject block3 = Instantiate<GameObject>(ResourceLoader.instance.scoreBlockObj);
        block3.transform.position = new Vector2(xPosition, yPosition);
        block3.GetComponent<ScoreBlock>().PopulateBlock(FishType.RED_BASS, count, value);
        yPosition -= yPositionChange;

        value = PointProcessor.instance.GetTroutValue(playerNum, out count);
        GameObject block4 = Instantiate<GameObject>(ResourceLoader.instance.scoreBlockObj);
        block4.transform.position = new Vector2(xPosition, yPosition);
        block4.GetComponent<ScoreBlock>().PopulateBlock(FishType.TROUT, count, value);
        yPosition -= yPositionChange;

        value = PointProcessor.instance.GetCatchValue(fishController, FishType.OYSTER, out count);
        GameObject block5 = Instantiate<GameObject>(ResourceLoader.instance.scoreBlockObj);
        block5.transform.position = new Vector2(xPosition, yPosition);
        block5.GetComponent<ScoreBlock>().PopulateBlock(FishType.OYSTER, count, value);
        yPosition -= yPositionChange;

        blocks[playerNum - 1].Enqueue(block1);
        blocks[playerNum - 1].Enqueue(block2);
        blocks[playerNum - 1].Enqueue(block3);
        blocks[playerNum - 1].Enqueue(block4);
        blocks[playerNum - 1].Enqueue(block5);

        //playerGear[player.playerNum - 1].gold += value;
    }

    public void UpdateScores()
    {
        int gold = StatsManager.instance.playerGear[0].gold;
        //scoreText.text = "GOLD: " + gold;
    }

}
