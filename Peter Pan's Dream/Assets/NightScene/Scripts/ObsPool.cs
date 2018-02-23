using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsPool : MonoBehaviour {

	/**
	 * [--Obstacle--]
	 * Tower			0
	 * Bat				1
	 * TowerWithBrick	2
	 * BatWithWave		3
     * TowerWFirework   4
     * Ghost            5
	 * 
	 * [--Collective--]
	 * 9SquareStar		800
	 * 
	 * [--Fairy--]
	 * Heal				900
     * Invincible       901
     * Magnet           902
	 */

	public enum Obstacle {
		Tower, 
		Bat,
        TowerWithBrick,
		BatWithWave,
        TowerWithFirework,
        Ghost
	};

	public enum Collective {
		Star = 800
	}

	public enum Fairy {
		Heal = 900,
        Invincible,
        Magnet
	}

    public GameObject towerPrefab;
    public GameObject batPrefab;
    public GameObject starPrefab;
	public GameObject towerWithBrickPrefab;
    public GameObject towerWithFireworkPrefab;
    public GameObject batWithWavePrefab;
    public GameObject ghostPrefab;
	public GameObject fairyWithHealPrefab;
    public GameObject fairyWithInvinciblePrefab;
    public GameObject fairyWithMagnetPrefab;
   
	public int type;

	private GameObject[] towers;
	private GameObject[] bats;
	private GameObject[] towerWithBricks;
	private GameObject[] batWithWaves;
    private GameObject[] towerWithFireworks;
    private GameObject[] ghosts;
	private GameObject[] stars;
	private GameObject fairyWithHeal;
    private GameObject fairyWithInvincible;
    private GameObject fairyWithMagnet;

	private Vector2 objectPoolPosition = new Vector2 (-15,-25);     //A holding position for our unused columns offscreen.
	private float spawnXPosition = 15f;

	//Main attributes.
	public readonly int DEFAULT_OBS_POOL_SIZE = 5;
	public float spawnRate = 2f;
    public readonly int GHOST_POOL_SIZE = 10;
    public float ghostSpawnRate = 0.8f;
    public readonly int obsTypeCountTotal = 6;
	public readonly int colTypeCountTotal = 1;
	public readonly int fairyTypeCountTotal = 3;

	//Costum attributes.
    private readonly float spawnRateMin = 1.5f;
    private readonly float spawnRateMax = 4f;
    private readonly float ghostSpawnRateMin = 1f;
    private readonly float ghostSpawnRateMax = 2f;
    private readonly float batYMin = -2f;
    private readonly float batYMax = 1.5f;
    private readonly float batWaveYMin = -3f;
    private readonly float batWaveYMax = 3.9f;
    private readonly float starYMin = -2f;
    private readonly float starYMax = 1.5f;
    private readonly float ghostYMin = -3f;
    private readonly float ghostYMax = 3.9f;
    private bool ghostStorm;

	//DO NOT CHANGE. DEFAULT ATTRIBUTES.
	private float curScore = 0f;
	public int[] curObsLocArr;
	public int[] curColLocArr;
	public int[] curFairyLocArr;
	//public int[] typeToPoolSize
	public int obsTypeCountCur = 1;
    public float timeSinceLastSpawned;
	public float timeSinceLastHeal;
    public float timeSinceLastInvincible;
    public float timeSinceLastMagnet;

	public ArrayList curObsTypeList = new ArrayList();
	public bool needToHeal = false;

    void GenerateObstacles(GameObject obsObj, GameObject[] obsObjArr, int poolSize) {
        for (int i = 0; i < poolSize; i++) {
			obsObjArr[i] = (GameObject)Instantiate(obsObj, objectPoolPosition, Quaternion.identity);
		}
	}

    void SetupObstacles(GameObject[] obsObjArr, Vector2 verAttr, int curObsTypeIndex, int poolSize) {
		int currentLocation = curObsLocArr [curObsTypeIndex];
		obsObjArr [currentLocation].transform.position = verAttr;
		currentLocation++;
		if (currentLocation >= poolSize) {
			currentLocation = 0;
		}
		curObsLocArr [curObsTypeIndex] = currentLocation;
	}

	void SetupCollectives(GameObject[] colObjArr, Vector2 verAttr, int curColTypeIndex, int poolSize) {
		curColTypeIndex -= 800;
		int currentLocation = curColLocArr [curColTypeIndex];
		colObjArr [currentLocation].transform.position = verAttr;
		currentLocation++;
		if (currentLocation >= poolSize) {
			currentLocation = 0;
		}
		curColLocArr [curColTypeIndex] = currentLocation;
	}

	void HealMechanism () {
		if (GameControl.instance.getHP () < 3) {
			fairyWithHeal.transform.position = new Vector2 (spawnXPosition, 0f);
		}
	}

    void Start() {
        ghostStorm = false;

        timeSinceLastSpawned = 0f;
		timeSinceLastHeal = 0f;
        timeSinceLastInvincible = 0f;
        timeSinceLastMagnet = 0f;

		curObsLocArr = new int[obsTypeCountTotal];
		curColLocArr = new int[colTypeCountTotal];
		curFairyLocArr = new int[fairyTypeCountTotal];

        towers = new GameObject[DEFAULT_OBS_POOL_SIZE];
        bats = new GameObject[DEFAULT_OBS_POOL_SIZE];
        towerWithBricks = new GameObject[DEFAULT_OBS_POOL_SIZE];
        stars = new GameObject[DEFAULT_OBS_POOL_SIZE];
        batWithWaves = new GameObject[DEFAULT_OBS_POOL_SIZE];
        towerWithFireworks = new GameObject[DEFAULT_OBS_POOL_SIZE];
        ghosts = new GameObject[GHOST_POOL_SIZE];

        GenerateObstacles(towerPrefab, towers, DEFAULT_OBS_POOL_SIZE);
        GenerateObstacles(batPrefab, bats, DEFAULT_OBS_POOL_SIZE);
        GenerateObstacles(towerWithBrickPrefab, towerWithBricks, DEFAULT_OBS_POOL_SIZE);
        GenerateObstacles(starPrefab, stars, DEFAULT_OBS_POOL_SIZE);
        GenerateObstacles(batWithWavePrefab, batWithWaves, DEFAULT_OBS_POOL_SIZE);
        GenerateObstacles(towerWithFireworkPrefab, towerWithFireworks, DEFAULT_OBS_POOL_SIZE);
        GenerateObstacles(ghostPrefab, ghosts, GHOST_POOL_SIZE);

		fairyWithHeal = (GameObject)Instantiate(fairyWithHealPrefab, objectPoolPosition, Quaternion.identity);
        fairyWithInvincible = (GameObject)Instantiate(fairyWithInvinciblePrefab, objectPoolPosition, Quaternion.identity);
        fairyWithMagnet = (GameObject)Instantiate(fairyWithMagnetPrefab, objectPoolPosition, Quaternion.identity);
    }

    //This spawns columns as long as the game is not over.
    void Update() {
		if (needToHeal) {
			timeSinceLastHeal += Time.deltaTime;
		}

        timeSinceLastInvincible += Time.deltaTime;
        timeSinceLastMagnet += Time.deltaTime;
        timeSinceLastSpawned += Time.deltaTime;

		curScore = GameControl.instance.score;

		if (curScore == 0f) {
			obsTypeCountCur = 3;
			curObsTypeList.Add (Obstacle.Bat);
			curObsTypeList.Add (Obstacle.Tower);
            curObsTypeList.Add (Fairy.Invincible);
		} else if (curScore == 15f) {
			curObsTypeList.Clear ();
			obsTypeCountCur = 2;
			curObsTypeList.Add (Obstacle.BatWithWave);
            curObsTypeList.Add (Fairy.Invincible);
        } else if (curScore == 30f) {
			curObsTypeList.Clear ();
			obsTypeCountCur = 3;
			curObsTypeList.Add (Obstacle.Bat);
			curObsTypeList.Add (Obstacle.Tower);
            curObsTypeList.Add (Fairy.Invincible);
        } else if (curScore == 45f) {
            curObsTypeList.Clear();
            obsTypeCountCur = 2;
            curObsTypeList.Add(Obstacle.Ghost);
            curObsTypeList.Add(Fairy.Heal);
            ghostStorm = true;
        } else if (curScore == 60f) {
            ghostStorm = false;
			curObsTypeList.Clear ();
			obsTypeCountCur = 7;
			curObsTypeList.Add (Obstacle.Tower);
			curObsTypeList.Add (Obstacle.Bat);
            curObsTypeList.Add (Obstacle.Ghost);
			curObsTypeList.Add (Collective.Star);
			curObsTypeList.Add (Fairy.Heal);
            curObsTypeList.Add (Fairy.Invincible);
            curObsTypeList.Add (Fairy.Magnet);
        } else if (curScore == 80f) {
			curObsTypeList.Clear ();
			obsTypeCountCur = 9;
			curObsTypeList.Add (Obstacle.Tower);
			curObsTypeList.Add (Obstacle.Bat);
			curObsTypeList.Add (Obstacle.TowerWithBrick);
			curObsTypeList.Add (Obstacle.BatWithWave);
			curObsTypeList.Add (Collective.Star);
			curObsTypeList.Add (Fairy.Heal);
            curObsTypeList.Add (Obstacle.TowerWithFirework);
            curObsTypeList.Add (Fairy.Invincible);
            curObsTypeList.Add (Fairy.Magnet);
        }

		// When detect hp lost, add FairyWithHeal into the pool.
		if (GameControl.instance.getHP () < 3 && !needToHeal) {
			needToHeal = true;
		}
		if (GameControl.instance.getHP () == 3 && needToHeal) {
			needToHeal = false;
		}

        if (GameControl.instance.gameOver == false && timeSinceLastSpawned >= spawnRate)  {
			
            timeSinceLastSpawned = 0f;
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
            if (ghostStorm) {
                spawnRate = Random.Range(ghostSpawnRateMin, ghostSpawnRateMax);
            }

			int typeIndex = Random.Range(0, obsTypeCountCur);
			if (curObsTypeList.Count == 0) {
				return;
			}
			type = (int)curObsTypeList [typeIndex];

			switch (type) {
			    case (int)Obstacle.Tower:
				    SetupObstacles (towers, new Vector2 (spawnXPosition, 0f), (int)Obstacle.Tower, DEFAULT_OBS_POOL_SIZE);
				    break;
			    case (int)Obstacle.Bat: 
                    SetupObstacles (bats, new Vector2 (spawnXPosition, Random.Range (batYMin, batYMax)), (int)Obstacle.Bat, DEFAULT_OBS_POOL_SIZE);
				    break;
			    case (int)Obstacle.TowerWithBrick:
                    SetupObstacles (towerWithBricks, new Vector2(spawnXPosition, 0f), (int)Obstacle.TowerWithBrick, DEFAULT_OBS_POOL_SIZE);
				    break;
			    case (int)Obstacle.BatWithWave:
                    SetupObstacles (batWithWaves, new Vector2(spawnXPosition, Random.Range (batWaveYMin, batWaveYMax)), (int)Obstacle.BatWithWave, DEFAULT_OBS_POOL_SIZE);
				    break;
                case (int)Obstacle.TowerWithFirework:
                    SetupObstacles (towerWithFireworks, new Vector2(spawnXPosition, -3.2f), (int)Obstacle.TowerWithFirework, DEFAULT_OBS_POOL_SIZE);
                    break;
                case (int)Obstacle.Ghost:
                    SetupObstacles(ghosts, new Vector2(spawnXPosition, Random.Range(ghostYMin, ghostYMax)), (int)Obstacle.Ghost, GHOST_POOL_SIZE);
                    break;
			    case (int)Collective.Star:
                    SetupCollectives (stars, new Vector2(spawnXPosition, Random.Range(starYMin, starYMax)), (int)Collective.Star, DEFAULT_OBS_POOL_SIZE);
                    break;
			    case (int)Fairy.Heal:
    				if (timeSinceLastHeal >= 30f && needToHeal) {
    					fairyWithHeal.transform.position = new Vector2 (spawnXPosition, 0f);
    					timeSinceLastHeal = 0f;
    				} else {
    					// When the FairyWithHeal doesn't need to show up, and index was randomed to be here, then, skip this heal and random again immediately.
    					timeSinceLastSpawned = spawnRate; 
    				}
    				break;
                case (int)Fairy.Invincible:
                    if (timeSinceLastInvincible >= 60f) {
                        fairyWithInvincible.transform.position = new Vector2(spawnXPosition, 0f);
                        timeSinceLastInvincible = 0f;
                    } else {
                        timeSinceLastSpawned = spawnRate;
                    }
                    break;
                case (int)Fairy.Magnet:
                    if (timeSinceLastMagnet >= 30f) {
                        fairyWithMagnet.transform.position = new Vector2(spawnXPosition, 0f);
                        timeSinceLastMagnet = 0f;
                    } else {
                        timeSinceLastSpawned = spawnRate;
                    }
                    break;
                }
        
        }
    }
}
