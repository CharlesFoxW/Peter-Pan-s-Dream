using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * STEP:
 * 1. Add your item to any one of the ENUM: Obstacle, Collective and Fairy;
 * 2. Create Prefab GameObject for your item;
 * 3. Create GameObject Array or single one;
 * 4. Modify type count.
 * 5. In Start(), initialize your GameObject Array or single one. And then, GenerateObstacle.
 * 6. In Update(), add your item to any score stage. And then, add your item case into SWITCH;
 **/

public class ObsPool : MonoBehaviour
{

    /**
     * [--Obstacle--]
     * Tower                0
     * Bat                  1
     * TowerWithBrick       2
     * BatWithWave          3
     * TowerWFirework       4
     * Ghost                5
     * 
     * [--Settings--]
     * Normal_1             700
     * Normal_2             701
     * Normal_3             702
     * Normal_4             703
     * GhostStorm           714
     * BatStorm             715
     * CoinStorm            716
     * 
     * [--Collective--]
     * SquareCoinGold      800
     * LineCoinGold        801
     * LineCoinBronze      802
     * LineCoinSilver      803
     * CoinGold            804
     * CoinBronze          805
     * CoinSilver          806        
     * 
     * [--Fairy--]
     * Heal                 900
     * Invincible           901
     * Magnet               902
     */

    public enum Obstacle
    {
        Tower,
        Bat,
        TowerWithBrick,
        BatWithWave,
        TowerWithFirework,
        Ghost
    }

    public enum CollectiveSettings
    {
        Normal_1 = 600
    }

    public enum Settings
    {
        Normal_1 = 700,
        Normal_2,
        Normal_3,
        Normal_4,
        GhostStorm = 714,
        BatWithWaveStorm,
        CoinStorm
    }

    public enum Collective
    {
        SquareCoinGold = 800,
        LineCoinGold,
        LineCoinBronze,
        LineCoinSilver,
        SingleCoinGold,
        SingleCoinBronze,
        SingleCoinSilver,
        PandoraBox = 850,
        Gem
    }

    public enum Fairy
    {
        Heal = 900,
        Invincible,
        Magnet
    }

    public GameObject towerPrefab;
    public GameObject batPrefab;
    public GameObject squareCoinGoldPrefab;
    public GameObject lineCoinGoldPrefab;
    public GameObject pandoraBoxPrefab;
    public GameObject towerWithBrickPrefab;
    public GameObject towerWithFireworkPrefab;
    public GameObject batWithWavePrefab;
    public GameObject ghostPrefab;
    public GameObject fairyWithHealPrefab;
    public GameObject fairyWithInvinciblePrefab;
    public GameObject fairyWithMagnetPrefab;

    public int type; // This is only for debug use.
    public int curSet; // This is only for debug use.

    private GameObject[] towers;
    private GameObject[] bats;
    private GameObject[] towerWithBricks;
    private GameObject[] batWithWaves;
    private GameObject[] towerWithFireworks;
    private GameObject[] ghosts;
    private GameObject[] squareCoinGolds;
    private GameObject[] lineCoinGolds;
    private GameObject pandoraBox;
    private GameObject fairyWithHeal;
    private GameObject fairyWithInvincible;
    private GameObject fairyWithMagnet;

    private Vector2 objectPoolPosition = new Vector2(-15, -25);     //A holding position for our unused columns offscreen.
    private float spawnXPosition = 15f; // Revival location for all items.

    //Main attributes <NEED TO BE TAKEN CARE>.
    private readonly int DEFAULT_OBS_POOL_SIZE = 5;
    private readonly int COIN_POOL_SIZE = 10;
    private float spawnRate = 2f;
    private float coinSpawnRate = float.MaxValue;
    private readonly int GHOST_POOL_SIZE = 10;
    private readonly int OBS_TYPE_COUNT_TOTAL = 6;
    private readonly int COL_TYPE_COUNT_TOTAL = 2;
    private readonly int FAIRY_TYP_COUNT_TOTAL = 3;
    private readonly int OBS_SET_COUNT = 20;
    private readonly int COL_SET_COUNT = 10;

    //Costum attributes <NEED TO BE TAKEN CARE>.
    private readonly float DEFAULT_SPAWN_RATE_MIN = 1.5f;
    private readonly float DEFAULT_SPAWN_RATE_MAX = 3f;
    private readonly float STORM_SPAWN_RATE_MIN = 0.7f;
    private readonly float STORM_SPAWN_RATE_MAX = 1.5f;
    private readonly float BAT_Y_MIN = -2f;
    private readonly float BAT_Y_MAX = 1.5f;
    private readonly float BAT_WAVE_Y_MIN = -3f;
    private readonly float BAT_WAVE_Y_MAX = 3.9f;
    private readonly float COL_Y_MIN = -2.5f;
    private readonly float COL_Y_MAX = 3.5f;
    private readonly float GHOST_Y_MIN = -3f;
    private readonly float GHOST_Y_MAX = 3.9f;

    //DEFAULT ATTRIBUTES <DO NOT CHANGE>.
    //private float curScore = 0f;
    public int[] curObsLocArr;
    public int[] curColLocArr;
    public int[] curFairyLocArr;
    public int obsTypeCountCur = 1;
    public float timeSinceStart;
    public float timeSinceLastPandoraBox;
    public float timeSinceLastCoin;
    public float timeSinceLastSpawned;
    public float timeSinceLastHeal;
    public float timeSinceLastInvincible;
    public float timeSinceLastMagnet;
    public ArrayList curObsTypeList;
    public bool needToHeal = false;
    public bool stormMode = false;
    private bool[] addedToObsList;
    private Setting[] obsSettingList;
    private Setting[] colSettingList;

    void Start()
    {
        timeSinceStart = 0f;
        timeSinceLastPandoraBox = 0f;
        timeSinceLastCoin = 0f;
        timeSinceLastSpawned = 0f;
        timeSinceLastHeal = 0f;
        timeSinceLastInvincible = 0f;
        timeSinceLastMagnet = 0f;

        curObsLocArr = new int[OBS_TYPE_COUNT_TOTAL];
        curColLocArr = new int[COL_TYPE_COUNT_TOTAL];
        curFairyLocArr = new int[FAIRY_TYP_COUNT_TOTAL];
        addedToObsList = new bool[OBS_SET_COUNT];
        obsSettingList = new Setting[OBS_SET_COUNT];
        colSettingList = new Setting[COL_SET_COUNT];

        towers = new GameObject[DEFAULT_OBS_POOL_SIZE];
        bats = new GameObject[DEFAULT_OBS_POOL_SIZE];
        towerWithBricks = new GameObject[DEFAULT_OBS_POOL_SIZE];
        squareCoinGolds = new GameObject[COIN_POOL_SIZE];
        lineCoinGolds = new GameObject[COIN_POOL_SIZE];
        batWithWaves = new GameObject[DEFAULT_OBS_POOL_SIZE];
        towerWithFireworks = new GameObject[DEFAULT_OBS_POOL_SIZE];
        ghosts = new GameObject[GHOST_POOL_SIZE];

        GenerateObstacles(towerPrefab, towers, DEFAULT_OBS_POOL_SIZE);
        GenerateObstacles(batPrefab, bats, DEFAULT_OBS_POOL_SIZE);
        GenerateObstacles(towerWithBrickPrefab, towerWithBricks, DEFAULT_OBS_POOL_SIZE);
        GenerateObstacles(squareCoinGoldPrefab, squareCoinGolds, COIN_POOL_SIZE);
        GenerateObstacles(lineCoinGoldPrefab, lineCoinGolds, COIN_POOL_SIZE);
        GenerateObstacles(batWithWavePrefab, batWithWaves, DEFAULT_OBS_POOL_SIZE);
        GenerateObstacles(towerWithFireworkPrefab, towerWithFireworks, DEFAULT_OBS_POOL_SIZE);
        GenerateObstacles(ghostPrefab, ghosts, GHOST_POOL_SIZE);

        pandoraBox = (GameObject)Instantiate(pandoraBoxPrefab, objectPoolPosition, Quaternion.identity);
        fairyWithHeal = (GameObject)Instantiate(fairyWithHealPrefab, objectPoolPosition, Quaternion.identity);
        fairyWithInvincible = (GameObject)Instantiate(fairyWithInvinciblePrefab, objectPoolPosition, Quaternion.identity);
        fairyWithMagnet = (GameObject)Instantiate(fairyWithMagnetPrefab, objectPoolPosition, Quaternion.identity);

        //Normal Setting 1: Tower, Bat, Star
        obsSettingList[0] = new Setting();
        obsSettingList[0].addItem((int)Obstacle.Tower);
        obsSettingList[0].addItem((int)Obstacle.Bat);
        obsSettingList[0].addItem((int)Fairy.Invincible);

        //Normal Setting 2: Tower, BatWithWave, Bat, Star, Ghost
        obsSettingList[1] = new Setting();
        obsSettingList[1].addItem((int)Obstacle.Tower);
        obsSettingList[1].addItem((int)Obstacle.BatWithWave);
        obsSettingList[1].addItem((int)Obstacle.Bat);
        obsSettingList[1].addItem((int)Obstacle.Ghost);
        obsSettingList[1].addItem((int)Fairy.Invincible);
        obsSettingList[1].addItem((int)Fairy.Magnet);
        obsSettingList[1].addItem((int)Fairy.Heal);

        //Normal Setting 3: TowerWithBrick, Bat, Star, Ghost
        obsSettingList[2] = new Setting();
        obsSettingList[2].addItem((int)Obstacle.TowerWithBrick);
        obsSettingList[2].addItem((int)Obstacle.Bat);
        obsSettingList[2].addItem((int)Obstacle.Ghost);
        obsSettingList[2].addItem((int)Fairy.Invincible);
        obsSettingList[2].addItem((int)Fairy.Magnet);
        obsSettingList[2].addItem((int)Fairy.Heal);

        //Normal Setting 4: TowerWithFirework, Star, Ghost
        obsSettingList[3] = new Setting();
        obsSettingList[3].addItem((int)Obstacle.TowerWithFirework);
        obsSettingList[3].addItem((int)Obstacle.Ghost);
        obsSettingList[3].addItem((int)Fairy.Invincible);
        obsSettingList[3].addItem((int)Fairy.Magnet);
        obsSettingList[3].addItem((int)Fairy.Heal);

        //Special Setting 1: Ghost Storm
        obsSettingList[14] = new Setting();
        obsSettingList[14].addItem((int)Obstacle.Ghost);
        obsSettingList[14].addItem((int)Fairy.Invincible);

        //Special Setting 2: Bat Storm
        obsSettingList[15] = new Setting();
        obsSettingList[15].addItem((int)Obstacle.BatWithWave);
        obsSettingList[15].addItem((int)Obstacle.Bat);
        obsSettingList[15].addItem((int)Fairy.Invincible);

        //Special Setting 3: Coin Storm
        obsSettingList[16] = new Setting();
        obsSettingList[16].addItem((int)Collective.SquareCoinGold);
        obsSettingList[16].addItem((int)Collective.LineCoinGold);
        obsSettingList[16].addItem((int)Fairy.Magnet);

        //Collection 1: 
        colSettingList[0] = new Setting();
        colSettingList[0].addItem((int)Collective.LineCoinGold);
        colSettingList[0].addItem((int)Collective.SquareCoinGold);
        colSettingList[0].addItem((int)Collective.PandoraBox);
    }

    //This spawns columns as long as the game is not over.
    void Update()
    {
        if (needToHeal)
        {
            timeSinceLastHeal += Time.deltaTime;
        }

        timeSinceStart += Time.deltaTime;
        timeSinceLastPandoraBox += Time.deltaTime;
        timeSinceLastCoin += Time.deltaTime;
        timeSinceLastInvincible += Time.deltaTime;
        timeSinceLastMagnet += Time.deltaTime;
        timeSinceLastSpawned += Time.deltaTime;

        if (timeSinceStart < 15f && !addedToObsList[0])
        {
            UsingSetting((int)Settings.Normal_1);
        }
        if (timeSinceStart > 15f && timeSinceStart < 30f && !addedToObsList[1])
        {
            UsingSetting((int)Settings.Normal_2);
        }
        if (timeSinceStart > 30f && timeSinceStart < 45f && !addedToObsList[(int)Settings.BatWithWaveStorm - 700])
        {
            UsingSetting((int)Settings.BatWithWaveStorm);
        }
        if (timeSinceStart > 45f && timeSinceStart < 60f && !addedToObsList[2])
        {
            UsingSetting((int)Settings.Normal_3);
        }
        if (timeSinceStart > 60f && timeSinceStart < 75f && !addedToObsList[(int)Settings.CoinStorm - 700])
        {
            UsingSetting((int)Settings.CoinStorm);
        }
        if (timeSinceStart > 75f && timeSinceStart < 90f && !addedToObsList[(int)Settings.GhostStorm - 700])
        {
            UsingSetting((int)Settings.GhostStorm);
        }
        if (timeSinceStart > 90f && !addedToObsList[3])
        {
            UsingSetting((int)Settings.Normal_4);
        }


        // When detect hp lost, add FairyWithHeal into the pool.
        if (GameControl.instance.getHP() < 3 && !needToHeal)
        {
            needToHeal = true;
        }
        if (GameControl.instance.getHP() == 3 && needToHeal)
        {
            needToHeal = false;
        }

        if (GameControl.instance.gameOver == false)
        {
            if (timeSinceLastSpawned >= spawnRate)
            {
                timeSinceLastSpawned = 0f;
                spawnRate = Random.Range(DEFAULT_SPAWN_RATE_MIN, DEFAULT_SPAWN_RATE_MAX);
                if (stormMode)
                {
                    spawnRate = Random.Range(STORM_SPAWN_RATE_MIN, STORM_SPAWN_RATE_MAX);
                }
                else
                {
                    if (spawnRate < 2f) 
                    {
                        coinSpawnRate = float.MaxValue;
                    } 
                    else 
                    {
                        coinSpawnRate = Random.Range(1f, spawnRate - 1f);
                    }
                    timeSinceLastCoin = 0f;
                }

                int typeIndex = Random.Range(0, obsTypeCountCur);
                if (curObsTypeList.Count == 0)
                {
                    return;
                }
                type = (int)curObsTypeList[typeIndex];
                LetItShow(type);
            }
            if (timeSinceLastCoin >= coinSpawnRate) {
                float tempLastCoin = timeSinceLastCoin;
                timeSinceLastCoin = 0f;
                coinSpawnRate = float.MaxValue;
                int colTypeIndex = (int)(colSettingList[0].typeList[Random.Range(0, 3)]);
                if (colTypeIndex == (int)Collective.PandoraBox) 
                {
                    if (timeSinceLastPandoraBox < 30f) 
                    {
                        timeSinceLastCoin = tempLastCoin;
                    } else {
                        timeSinceLastCoin = 0f;
                        timeSinceLastPandoraBox = 0f;
                        LetItShow(colTypeIndex);
                        return;
                    }
                }
                LetItShow(colTypeIndex);
            }
        }
    }

    // Additional Methods.
    void GenerateObstacles(GameObject obsObj, GameObject[] obsObjArr, int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            obsObjArr[i] = (GameObject)Instantiate(obsObj, objectPoolPosition, Quaternion.identity);
        }
    }

    void SetupObstacles(GameObject[] obsObjArr, Vector2 verAttr, int curObsTypeIndex, int poolSize)
    {
        int currentLocation = curObsLocArr[curObsTypeIndex];
        obsObjArr[currentLocation].transform.position = verAttr;
        currentLocation++;
        if (currentLocation >= poolSize)
        {
            currentLocation = 0;
        }
        curObsLocArr[curObsTypeIndex] = currentLocation;
    }

    void SetupCollectives(GameObject[] colObjArr, Vector2 verAttr, int curColTypeIndex, int poolSize)
    {
        curColTypeIndex -= 800;
        int currentLocation = curColLocArr[curColTypeIndex];
        colObjArr[currentLocation].transform.position = verAttr;
        currentLocation++;
        if (currentLocation >= poolSize)
        {
            currentLocation = 0;
        }
        curColLocArr[curColTypeIndex] = currentLocation;
    }

    void HealMechanism()
    {
        if (GameControl.instance.getHP() < 3)
        {
            fairyWithHeal.transform.position = new Vector2(spawnXPosition, 0f);
        }
    }

    void UsingSetting(int setIndex)
    {
        curSet = setIndex;
        setIndex -= 700;
        if (setIndex >= 14)
        {
            stormMode = true;
        }
        else
        {
            stormMode = false;
        }
        curObsTypeList = obsSettingList[setIndex].typeList;
        obsTypeCountCur = curObsTypeList.Count;
        addedToObsList[setIndex] = true;
    }

    void LetItShow(int typeIndex)
    {
        switch (typeIndex)
        {
            case (int)Obstacle.Tower:
                SetupObstacles(towers, new Vector2(spawnXPosition, 0f), (int)Obstacle.Tower, DEFAULT_OBS_POOL_SIZE);
                break;
            case (int)Obstacle.Bat:
                SetupObstacles(bats, new Vector2(spawnXPosition, Random.Range(BAT_Y_MIN, BAT_Y_MAX)), (int)Obstacle.Bat, DEFAULT_OBS_POOL_SIZE);
                break;
            case (int)Obstacle.TowerWithBrick:
                SetupObstacles(towerWithBricks, new Vector2(spawnXPosition, 0f), (int)Obstacle.TowerWithBrick, DEFAULT_OBS_POOL_SIZE);
                break;
            case (int)Obstacle.BatWithWave:
                SetupObstacles(batWithWaves, new Vector2(spawnXPosition, Random.Range(BAT_WAVE_Y_MIN, BAT_WAVE_Y_MAX)), (int)Obstacle.BatWithWave, DEFAULT_OBS_POOL_SIZE);
                break;
            case (int)Obstacle.TowerWithFirework:
                SetupObstacles(towerWithFireworks, new Vector2(spawnXPosition, -3.2f), (int)Obstacle.TowerWithFirework, DEFAULT_OBS_POOL_SIZE);
                break;
            case (int)Obstacle.Ghost:
                SetupObstacles(ghosts, new Vector2(spawnXPosition, Random.Range(GHOST_Y_MIN, GHOST_Y_MAX)), (int)Obstacle.Ghost, GHOST_POOL_SIZE);
                break;
            case (int)Collective.SquareCoinGold:
                SetupCollectives(squareCoinGolds, new Vector2(spawnXPosition, Random.Range(COL_Y_MIN, COL_Y_MAX)), (int)Collective.SquareCoinGold, COIN_POOL_SIZE);
                break;
            case (int)Collective.LineCoinGold:
                SetupCollectives(lineCoinGolds, new Vector2(spawnXPosition, Random.Range(COL_Y_MIN, COL_Y_MAX)), (int)Collective.LineCoinGold, COIN_POOL_SIZE);
                break;
            case (int)Collective.PandoraBox:
                pandoraBox.transform.position = new Vector2(spawnXPosition, Random.Range(COL_Y_MIN, COL_Y_MAX));
                break;
            case (int)Fairy.Heal:
                if (timeSinceLastHeal >= 30f && needToHeal)
                {
                    fairyWithHeal.transform.position = new Vector2(spawnXPosition, 0f);
                    timeSinceLastHeal = 0f;
                }
                else
                {
                    // When the FairyWithHeal doesn't need to show up, and index was randomed to be here, then, skip this heal and random again immediately.
                    timeSinceLastSpawned = spawnRate;
                }
                break;
            case (int)Fairy.Invincible:
                if (timeSinceLastInvincible >= 60f)
                {
                    fairyWithInvincible.transform.position = new Vector2(spawnXPosition, 0f);
                    timeSinceLastInvincible = 0f;
                }
                else
                {
                    timeSinceLastSpawned = spawnRate;
                }
                break;
            case (int)Fairy.Magnet:
                if (timeSinceLastMagnet >= 30f)
                {
                    fairyWithMagnet.transform.position = new Vector2(spawnXPosition, 0f);
                    timeSinceLastMagnet = 0f;
                }
                else
                {
                    timeSinceLastSpawned = spawnRate;
                }
                break;
        }
    }
}

class Setting
{
    public ArrayList typeList;

    public Setting()
    {
        typeList = new ArrayList();
    }
    public void addItem(int itemIndex)
    {
        typeList.Add(itemIndex);
    }
}
