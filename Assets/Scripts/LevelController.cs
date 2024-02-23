using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{   
    PuckMovementController puckMovementController;
    PaddleController paddleController;
    GameController gameController;
    GameObject blocksParent;
    AudioSource blockDestroySfx;
    LevelManager levelManager;

    public GameObject weakBlock;
    public GameObject strongBlock;
    public GameObject permanentBlock;
    public GameObject explosiveBlock;
    
    private GameObject[,] blocks;
    private int blocksRemaining;
    public int currentLevel;
    private bool levelsComplete;

    private static int ROWS = 4;
    private static int COLUMNS = 16;
    private static string[] LEVELS = {
        "_WWW__WWWW__WWW_" +
        "W___WW____WW___W" +
        "_WWW__WWWW__WWW_" +
        "________________",

        "W_W_W_WSSW_W_W_W" +
        "_S____WSSW____S_" +
        "__WWWP____PWWW__" +
        "__PWWW____WWWP__",

        "PWWWWWWWWWWWWWWP" +
        "P__W__PSSP__W__P" +
        "PSWS__WSSW__SWSP" +
        "PWEWWWWWWWWWWEWP",
    };

    public GameObject speedIncreasePowerup;
    public GameObject paddleIncreasePowerup;
    public GameObject explosionPowerup;

    void Start()
    {
        blocks = new GameObject[ROWS, COLUMNS];
        GameObject lm = GameObject.Find("LevelManager");
        if (lm != null) { levelManager = lm.GetComponent<LevelManager>(); }        
        puckMovementController = GameObject.Find("Puck").GetComponent<PuckMovementController>();
        paddleController = GameObject.Find("Paddle").GetComponent<PaddleController>();
        gameController = gameObject.GetComponent<GameController>();
        blockDestroySfx = gameObject.GetComponent<AudioSource>();
        blocksParent = GameObject.Find("Blocks");
        if (levelManager != null) {
            currentLevel = levelManager.level;
        } else {
            currentLevel = 0;
        }
        
        levelsComplete = false;
        LoadLevel(currentLevel);
    }

    void Update() {
        if (Input.GetKey("r")) {
            ResetLevel();
        }
    }

    void LoadLevel(int levelIndex) {
        string currentLevel = LEVELS[levelIndex];
        blocksRemaining = 0;
        for (int y = 0; y < ROWS; y++) {
            for (int x = 0; x < COLUMNS; x++) {
                GameObject b = null;
                int blockIndex = y*16 + x;
                char block = currentLevel[blockIndex];
                string powerup = "";
                switch (block) {
                    case '_': continue;
                    case 'P': b = Instantiate(permanentBlock); break;
                    case 'E': b = Instantiate(explosiveBlock); powerup = RandomPowerup(); blocksRemaining++; break;
                    case 'W': b = Instantiate(weakBlock); powerup = RandomPowerup(); blocksRemaining++; break;
                    case 'S': b = Instantiate(strongBlock); powerup = RandomPowerup(); blocksRemaining++; break;
                }
                if (b != null) {
                    BlockBehaviour behaviour = b.GetComponent<BlockBehaviour>();
                    if (powerup != "") { AddPowerup(powerup, b, behaviour); }
                    behaviour.x = x;
                    behaviour.y = y;
                    b.transform.position = new Vector3(x, y * 0.5f, 0);
                    b.transform.SetParent(blocksParent.transform, false);
                }
                blocks[y, x] = b;
            }
        }
    }

    void AddPowerup(string powerupName, GameObject block, BlockBehaviour blockBehaviour) {
        GameObject powerup = GetPowerup(powerupName);
        if (powerup == null) { return; }
        GameObject powerupObject = Instantiate(powerup);
        powerupObject.transform.parent = block.transform;
        powerupObject.transform.position = Vector3.zero;
        BasePowerup powerupBehaviour = powerupObject.GetComponent<BasePowerup>();
        blockBehaviour.SetPowerup(powerupBehaviour);
    }

    public string RandomPowerup() {
        float val = Random.Range(1, 15);
        if (val >= 1 && val < 3) { return "speed"; } 
        else if (val >= 3 && val < 7) { return "increasePaddle"; }
        else if (val == 7) { return "explosive"; }
        else { return ""; }
    }

    public GameObject GetPowerup(string powerup) {
        switch (powerup) {
            case "speed": return speedIncreasePowerup;
            case "increasePaddle": return paddleIncreasePowerup;
            case "explosive": return explosionPowerup;
            default: return null;
        }
    }

    public void DestroyBlock(int x, int y) {
        // Removes a block from the world!
        // Blocks call this when their internal logic dictates that they are destroyed
        if (blocks[y, x] != null) { // Ensure block exists...
            gameController.IncrementScore();
            blockDestroySfx.Play();
            Vector3 location = blocks[y,x].transform.position;
            Destroy(blocks[y, x]);
            blocksRemaining--;
            blocks[y,x] = null;
        }
    }

    void DestoryAll() {
        foreach (GameObject block in blocks) {
            if (block != null) { Destroy(block); }
        }
        blocks = new GameObject[ROWS, COLUMNS];
    }

    public void NextLevel() {
        currentLevel++;
        DestoryAll();
        if (currentLevel >= LEVELS.Length) {
            levelsComplete = true;
        } else {
            LoadLevel(currentLevel);
        }
    }

    public void ResetLevel() {
        DestoryAll();
        LoadLevel(currentLevel);
    }

    public int GetBlocksRemaining() {
        return blocksRemaining;
    }

    public bool LevelComplete() {
        return blocksRemaining == 0;
    }

    public bool GameComplete() {
        return levelsComplete;
    }

}
