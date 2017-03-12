using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Handles coordinating and displaying scores in the score scene
public class ScoreManager : MonoBehaviour
{

    private bool isRevealingScores = true;
    private bool finishedRevealing = false;
    private float revealRate = 0.5f;
    private float revealElapsedTime;
    private float startingYPosition = 4f;
    private float yPositionChange = 0.52f;

    private List<Queue<GameObject>> blocks;
    private bool[] activatedShops;

    // Use this for initialization
    void Awake()
    {
        activatedShops = new bool[5];

        blocks = new List<Queue<GameObject>>();
        for (int i = 0; i < 4; i++)
        {
            blocks.Add(new Queue<GameObject>());
        }
    }

    void Start()
    {
        GetComponent<ScoreTester>().RunTests();

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
            // Force 1-time logic to happen when blocks are finished showing
            if (!finishedRevealing)
            {
                ActivateCursors();
                isRevealingScores = false;
                finishedRevealing = true;
            }
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
                    ActivateShop(i);
                }
            }
        }
    }

    private void ActivateCursors()
    {
        for (int i = 1; i < 5; i++)
        {
            GameObject scorePanel = GameObject.Find("ScorePanel" + i);
            scorePanel.transform.Find("Cursor").gameObject.SetActive(true);
            scorePanel.transform.Find("ReadyCanvas/Shop").gameObject.SetActive(true);
        }
    }

    private void ActivateShop(int playerNum)
    {
        // Check if player is already in shop
        if (activatedShops[playerNum])
        {
            return;
        }

        GameObject scorePanel = GameObject.Find("ScorePanel" + playerNum);

        // Destroy score blocks
        ScoreBlock[] blocks = scorePanel.GetComponentsInChildren<ScoreBlock>();
        foreach (ScoreBlock block in blocks)
        {
            Destroy(block.gameObject);
        }

        // Change title to SHOP
        Transform titleText = scorePanel.transform.Find("Canvas").Find("Title");
        titleText.GetComponent<Text>().text = "SHOP";

        // Start shop
        scorePanel.transform.Find("ShopManager").gameObject.SetActive(true);
        scorePanel.transform.Find("ItemTitle").gameObject.SetActive(true);
        scorePanel.transform.Find("ItemCanvas").gameObject.SetActive(true);
        scorePanel.transform.Find("ReadyCanvas/Shop").gameObject.SetActive(false);
        scorePanel.transform.Find("ReadyCanvas/Ready").gameObject.SetActive(true);
        scorePanel.transform.Find("Cursor/CursorImage").GetComponent<Cursor>().allowInputs = true;
        activatedShops[playerNum] = true;
    }

    public void ProcessCatches(List<FishController> fishController, int playerNum)
    {
        int totalValue = 0;
        int value = 0;
        int count = 0;
        float yPosition = startingYPosition;
        float xPosition = -11.25f + 4.5f * playerNum;

        value = PointProcessor.instance.GetCatchValue(fishController, FishType.GREEN_BASS, out count);
        GameObject block1 = CreateScoreBlock(value, count, FishType.GREEN_BASS, xPosition, yPosition, playerNum);
        yPosition -= yPositionChange;
        totalValue += value;

        value = PointProcessor.instance.GetCatchValue(fishController, FishType.BLUE_BASS, out count);
        GameObject block2 = CreateScoreBlock(value, count, FishType.BLUE_BASS, xPosition, yPosition, playerNum);
        yPosition -= yPositionChange;
        totalValue += value;

        value = PointProcessor.instance.GetCatchValue(fishController, FishType.RED_BASS, out count);
        GameObject block3 = CreateScoreBlock(value, count, FishType.RED_BASS, xPosition, yPosition, playerNum);
        yPosition -= yPositionChange;
        totalValue += value;

        value = PointProcessor.instance.GetTroutValue(playerNum, out count);
        GameObject block4 = CreateScoreBlock(value, count, FishType.TROUT, xPosition, yPosition, playerNum);
        yPosition -= yPositionChange;
        totalValue += value;

        value = PointProcessor.instance.GetCatchValue(fishController, FishType.OYSTER, out count);
        GameObject block5 = CreateScoreBlock(value, count, FishType.OYSTER, xPosition, yPosition, playerNum);
        yPosition -= yPositionChange;
        totalValue += value;

        value = PointProcessor.instance.GetCatchValue(fishController, FishType.TIGER_SHARK, out count);
        GameObject block6 = CreateScoreBlock(value, count, FishType.TIGER_SHARK, xPosition, yPosition, playerNum);
        yPosition -= yPositionChange;
        totalValue += value;

        value = PointProcessor.instance.GetCatchValue(fishController, FishType.GREAT_WHITE_SHARK, out count);
        GameObject block7 = CreateScoreBlock(value, count, FishType.GREAT_WHITE_SHARK, xPosition, yPosition, playerNum);
        yPosition -= yPositionChange;
        totalValue += value;

        value = PointProcessor.instance.GetCatchValue(fishController, FishType.SALMON, out count);
        GameObject block8 = CreateScoreBlock(value, count, FishType.SALMON, xPosition, yPosition, playerNum);
        yPosition -= yPositionChange;
        totalValue += value;

        value = PointProcessor.instance.GetCatchValue(fishController, FishType.PUFFER, out count);
        GameObject block9 = CreateScoreBlock(value, count, FishType.PUFFER, xPosition, yPosition, playerNum);
        yPosition -= yPositionChange;
        totalValue += value;

        value = PointProcessor.instance.GetCatchValue(fishController, FishType.JELLYFISH, out count);
        GameObject block10 = CreateScoreBlock(value, count, FishType.JELLYFISH, xPosition, yPosition, playerNum);
        yPosition -= yPositionChange;
        totalValue += value;

        value = PointProcessor.instance.GetCatchValue(fishController, FishType.LOBSTER, out count);
        GameObject block11 = CreateScoreBlock(value, count, FishType.LOBSTER, xPosition, yPosition, playerNum);
        yPosition -= yPositionChange;
        totalValue += value;

        value = PointProcessor.instance.GetCatchValue(fishController, FishType.WHALE, out count);
        GameObject block12 = CreateScoreBlock(value, count, FishType.WHALE, xPosition, yPosition, playerNum);
        yPosition -= yPositionChange;
        totalValue += value;

        GameObject scoreBlock = Instantiate<GameObject>(ResourceLoader.instance.scoreBlockObj);
        scoreBlock.transform.position = new Vector2(xPosition, yPosition);
        scoreBlock.transform.SetParent(GameObject.Find("ScorePanel" + playerNum).transform);
        ScoreBlock totalBlock = scoreBlock.GetComponent<ScoreBlock>();
        totalBlock.nameText.text = "Total";
        totalBlock.countText.text = " ";
        totalBlock.valueText.text = "$" + totalValue;
        yPosition -= yPositionChange;

        blocks[playerNum - 1].Enqueue(block1);
        blocks[playerNum - 1].Enqueue(block2);
        blocks[playerNum - 1].Enqueue(block3);
        blocks[playerNum - 1].Enqueue(block4);
        blocks[playerNum - 1].Enqueue(block5);
        blocks[playerNum - 1].Enqueue(block6);
        blocks[playerNum - 1].Enqueue(block7);
        blocks[playerNum - 1].Enqueue(block8);
        blocks[playerNum - 1].Enqueue(block9);
        blocks[playerNum - 1].Enqueue(block10);
        blocks[playerNum - 1].Enqueue(block11);
        blocks[playerNum - 1].Enqueue(block12);
        blocks[playerNum - 1].Enqueue(scoreBlock);

        //playerGear[player.playerNum - 1].gold += value;
    }

    private GameObject CreateScoreBlock(int value, int count, FishType type, float xPosition, float yPosition, int playerNum)
    {
        GameObject block = Instantiate<GameObject>(ResourceLoader.instance.scoreBlockObj);
        block.transform.position = new Vector2(xPosition, yPosition);
        block.GetComponent<ScoreBlock>().PopulateBlock(type, count, value);
        block.transform.SetParent(GameObject.Find("ScorePanel" + playerNum).transform);

        return block;
    }

    public void UpdateScores()
    {
        int gold = StatsManager.instance.playerGear[0].gold;
        //scoreText.text = "GOLD: " + gold;
    }

}
