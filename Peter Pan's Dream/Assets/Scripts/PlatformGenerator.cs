using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

	public GameObject thePlatform;
	public Transform generationPoint;
	public float distanceBetween;

	private float platformWidth;

	public float distanceBetweenMin;
	public float distanceBetweenMax;

	private int platformSelector;
	private float[] platformWidths;
	private float[] platformHeights;


	public ObjectPooler[] theObjectPools;

	List<int>[] platformByLevel;

	private float minHeight;
	private float maxHeight;
	public Transform maxHeightPoint;
	public float maxHeightChange;
	private float heightChange;
	public float heightLevel;
	private float prevLevel = 0;
	private float curSameLevelNum = 0;
	private float maxSameLevelNum = 3;
	private float heightOffset = -0.85f;

	public bool isFirstGround = true;


	//coin generator parameters
	private CoinGenerator theCoinGenerator;
	public float randomCoinThreshold; 

	//ladybug generator:
	public float randomLadyBugThreshold;
	public ObjectPooler ladyBugPool;

	//sniper generator:
	public float randomSniperThreshold;
	public ObjectPooler sniperPool;


	// Use this for initialization
	void Start () {

		platformWidths = new float[theObjectPools.Length];
		platformHeights = new float[theObjectPools.Length];

		for (int i = 0; i < theObjectPools.Length; i++) {
			platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
			platformHeights[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.y;

		}

		minHeight = transform.position.y;
		maxHeight = maxHeightPoint.position.y;

		platformByLevel = new List<int>[3];
		//platformByLevel[0] = new List<int>{8};
		//platformByLevel[1] = new List<int>{8};
		//platformByLevel[2] = new List<int>{8};
		platformByLevel[0] = new List<int>{6, 7, 8};
		platformByLevel[1] = new List<int>{0, 1, 2, 3, 6};
		platformByLevel[2] = new List<int>{0, 1, 2, 3, 4, 5};

		theCoinGenerator = FindObjectOfType<CoinGenerator> ();

	}
	
	// Update is called once per frame
	void Update () {
		
		
		if (transform.position.x < generationPoint.position.x){
			

			int level = Random.Range (0, 3);

			if (level != prevLevel) {
				curSameLevelNum = 1;
			} else if (curSameLevelNum < maxSameLevelNum) {
				curSameLevelNum++;
			} else {
				while (level == prevLevel) {
					level = Random.Range (0, 2);
				}
				curSameLevelNum = 1;
			}
			if (prevLevel == level || isFirstGround) {
				distanceBetween = Random.Range (1.5f, distanceBetweenMax);
				isFirstGround = false;
			} else {
				distanceBetween = Random.Range (distanceBetweenMin, distanceBetweenMax);
			}
				
			prevLevel = level;

			platformSelector = platformByLevel[level][Random.Range(0, platformByLevel[level].Count)];


			heightChange = (float)(level - 1) * heightLevel + heightOffset;

			transform.position = new Vector3 (transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween,
				heightChange , transform.position.z);

			GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();
			newPlatform.transform.position = new Vector3 (
				transform.position.x,
				heightChange - platformHeights[platformSelector] / 2,
				transform.position.z
			);
			newPlatform.transform.rotation = transform.rotation;
			newPlatform.SetActive (true);

			Vector3 ladyBugPositionOffset = new Vector3 (0f, 0.6f, 0f);
			Vector3 coinPositionOffset = new Vector3 (0f, 1f, 0f);


			if (Random.Range (0f, 100f) < randomCoinThreshold) {
				theCoinGenerator.SpawnCoins (
					transform.position + coinPositionOffset,
					platformWidths[platformSelector],
					newPlatform.GetComponent<Renderer>().bounds);
			}

			if (platformWidths [platformSelector] > 5.2f) {
				
				if (Random.Range (0f, 100f) < randomSniperThreshold) {

					GameObject newSniper = sniperPool.GetPooledObject ();

					float sniperXPositionOffset = Random.Range (0f, platformWidths[platformSelector] / 2);
					Vector3 sniperPositionOffset = new Vector3 (sniperXPositionOffset, 1f, 0f);
					newSniper.transform.position = transform.position + sniperPositionOffset;
					newSniper.transform.rotation = transform.rotation;
					newSniper.SetActive (true);

				} else if (Random.Range (0f, 100f) < randomLadyBugThreshold) {
					
					GameObject newLadyBug = ladyBugPool.GetPooledObject ();


					newLadyBug.transform.position = transform.position + ladyBugPositionOffset;
					newLadyBug.transform.rotation = transform.rotation;

					// pass the parameters used for moving the ladybugs.
					LadyBugController.PassParameters (newLadyBug, newPlatform.transform.position, 
						newPlatform.GetComponent<BoxCollider2D> ().size.x);
					newLadyBug.SetActive (true);

				}

			}


			transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), 
				transform.position.y, transform.position.z);
		}
	}
}
