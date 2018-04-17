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

    //Healing Stars:
    public float randomHealingStarThreshold;
    public ObjectPooler HealingStarPool;

	//ladybug generator:
	public float randomLadyBugThreshold;
	public ObjectPooler ladyBugPool;

	//sniper generator:
	public float randomSniperThreshold;
	public ObjectPooler sniperPool;

	//Campfire generator:
	public float randomCampfireThreshold;
	public ObjectPooler campfirePool;

	public float randomCaptainHookThreshold;
	public ObjectPooler captainHookPool;

	public ObjectPooler ropePool;

	public ObjectPooler blackholePool;


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
		//platformByLevel[2] = new List<int>{4, 5};
		platformByLevel[0] = new List<int>{6, 7, 8};
		platformByLevel[1] = new List<int>{0, 1, 2, 3};
		platformByLevel[2] = new List<int>{0, 1, 2, 3, 4, 5};

		theCoinGenerator = FindObjectOfType<CoinGenerator> ();

	}
	
	// Update is called once per frame
	void Update () {
		
		
		if (transform.position.x < generationPoint.position.x){
			
			bool isReadyForBlackhole = true;
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

			//level = 2;

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


			Bounds platformBounds = newPlatform.GetComponent<Renderer> ().bounds;
			Bounds sniperBounds = new Bounds();
			Bounds starBounds = new Bounds();
			Bounds campfireBounds = new Bounds();
			Bounds captainHookBounds = new Bounds();

			if (SceneControl.Instance.HP < 3 && Random.Range(0f, 100f) < randomHealingStarThreshold) {
                
                GameObject newHealingStar = HealingStarPool.GetPooledObject();

                float starXPositionOffset = Random.Range(-platformWidths[platformSelector] / 2, platformWidths[platformSelector] / 2);
                Vector3 sniperPositionOffset = new Vector3(starXPositionOffset, 0.5f, 0f);
                newHealingStar.transform.position = transform.position + sniperPositionOffset;
                newHealingStar.transform.rotation = transform.rotation;
                newHealingStar.SetActive(true);
				starBounds = newHealingStar.GetComponent<Renderer> ().bounds;
				isReadyForBlackhole = false;
            }

			if (platformSelector == 4 || platformSelector == 5) {
				GameObject ropeLeft = ropePool.GetPooledObject ();
				ropeLeft.SetActive (true);
				GameObject ropeRight = ropePool.GetPooledObject ();
				ropeRight.SetActive (true);

				float xOffset = platformWidths [platformSelector] / 2 - 0.1f;
				float yOffset = 3;

				ropeLeft.transform.position = transform.position + new Vector3(-xOffset, yOffset, 0);
				ropeRight.transform.position = transform.position + new Vector3(xOffset, yOffset, 0);
			}


			if (platformWidths [platformSelector] > 5.2f) {
				
				if (Random.Range (0f, 100f) < randomSniperThreshold) {

					GameObject newSniper = sniperPool.GetPooledObject ();

					float sniperXPositionOffset = Random.Range (0f, platformWidths[platformSelector] / 2);
					Vector3 sniperPositionOffset = new Vector3 (sniperXPositionOffset, 1f, 0f);
					newSniper.transform.position = transform.position + sniperPositionOffset;
					newSniper.transform.rotation = transform.rotation;
					newSniper.SetActive (true);
					sniperBounds = newSniper.GetComponent<Renderer> ().bounds;
					isReadyForBlackhole = false;
				} else if (Random.Range (0f, 100f) < randomLadyBugThreshold) {
					
					GameObject newLadyBug = ladyBugPool.GetPooledObject ();


					newLadyBug.transform.position = transform.position + ladyBugPositionOffset;
					newLadyBug.transform.rotation = transform.rotation;

					// pass the parameters used for moving the ladybugs.
					LadyBugController.PassParameters (newLadyBug, newPlatform.transform.position, 
					newPlatform.GetComponent<BoxCollider2D> ().size.x);
					newLadyBug.SetActive (true);
					isReadyForBlackhole = false;
				} else if (Random.Range (0f, 100f) < randomCampfireThreshold) {

					GameObject newCampfire = campfirePool.GetPooledObject ();
					float campfireXPositionOffset = Random.Range (0f, platformWidths[platformSelector] / 2);
					Vector3 campfirePositionOffset = new Vector3 (campfireXPositionOffset, 0.7f, 0f);
					newCampfire.transform.position = transform.position + campfirePositionOffset;
					newCampfire.transform.rotation = transform.rotation;
					newCampfire.SetActive (true);
					campfireBounds = newCampfire.GetComponent<Renderer> ().bounds;
					isReadyForBlackhole = false;
				}	else if (Random.Range (0f, 100f) < randomCaptainHookThreshold) {

					GameObject newCaptainHook = captainHookPool.GetPooledObject ();
					float captainHookXPositionOffset = Random.Range (0f, platformWidths[platformSelector] / 2);
					Vector3 captainHookPositionOffset = new Vector3 (captainHookXPositionOffset, 2.5f, 0f);
					newCaptainHook.transform.position = transform.position + captainHookPositionOffset;
					newCaptainHook.transform.rotation = transform.rotation;
					newCaptainHook.SetActive (true);
					captainHookBounds = newCaptainHook.GetComponent<Renderer> ().bounds;
					isReadyForBlackhole = false;
				}

			}

			if (Random.Range (0f, 100f) < randomCoinThreshold) {
				theCoinGenerator.SpawnCoins (
					transform.position + coinPositionOffset,
					platformWidths[platformSelector],
					platformBounds,
					sniperBounds,
					starBounds,
					campfireBounds,
					captainHookBounds
				);
				isReadyForBlackhole = false;
			}

			if (level < 1.5 && isReadyForBlackhole && DaytimeControl.timepast >= 45f) {
				GameObject newBlackhole = blackholePool.GetPooledObject ();
				Vector3 blackholePositionOffset = new Vector3 (0f, 2f, 0f);
				newBlackhole.transform.position = transform.position + blackholePositionOffset;
				newBlackhole.transform.rotation = transform.rotation;
				newBlackhole.SetActive (true);
				DaytimeControl.timepast = 0f;
			}

			transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), 
				transform.position.y, transform.position.z);
		}
	}
}
