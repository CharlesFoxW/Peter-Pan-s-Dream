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

	public ObjectPooler[] theObjectPools;

	private float minHeight;
	private float maxHeight;
	public Transform maxHeightPoint;
	public float maxHeightChange;
	private float heightChange;
	public float heightLevel;

	//coin generator parameters
	private CoinGenerator theCoinGenerator;
	public float randomCoinThreshold; 

	//ladybug generator:
	public float randomLadyBugThreshold;
	public ObjectPooler ladyBugPool;


	// Use this for initialization
	void Start () {

		platformWidths = new float[theObjectPools.Length];
		for (int i = 0; i < theObjectPools.Length; i++) {
			platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
		}

		minHeight = transform.position.y;
		maxHeight = maxHeightPoint.position.y;

		theCoinGenerator = FindObjectOfType<CoinGenerator> ();
	}
	
	// Update is called once per frame
	void Update () {

		
		if (transform.position.x < generationPoint.position.x){
			distanceBetween = Random.Range (distanceBetweenMin, distanceBetweenMax);

			platformSelector = Random.Range (0, theObjectPools.Length);

			int level = Random.Range (0, 2);

			//heightChange = transform.position.y + Random.Range (maxHeightChange, -maxHeightChange);
			float heightOffset = -2f;

			if (platformSelector < 4) {	// grass platforms
				heightChange = (float)(level - 1) * heightLevel + heightOffset;
			} else {	// wood platforms
				heightChange = heightLevel + heightOffset;
			}

			transform.position = new Vector3 (transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween,
				heightChange, transform.position.z);


			GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();
			newPlatform.transform.position = transform.position;
			newPlatform.transform.rotation = transform.rotation;
			newPlatform.SetActive (true);

			if (Random.Range (0f, 100f) < randomCoinThreshold) {
				theCoinGenerator.SpawnCoins (new Vector3 (transform.position.x,
					transform.position.y + 1f, transform.position.z));
			}

			if (Random.Range (0f, 100f) < randomLadyBugThreshold) {
				
				if (platformWidths [platformSelector] > 5.2f) {
					GameObject newLadyBug = ladyBugPool.GetPooledObject ();

					Vector3 ladyBugPositionOffset = new Vector3 (0f, 1f, 0f);

					newLadyBug.transform.position = transform.position + ladyBugPositionOffset;
					newLadyBug.transform.rotation = transform.rotation;

					// pass the parameters used for moving the ladybugs.
					LadyBugController.PassParameters(newLadyBug, newPlatform.transform.position, 
						newPlatform.GetComponent<BoxCollider2D>().size.x);
					newLadyBug.SetActive (true);
				}
			}

			transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), 
				transform.position.y, transform.position.z);
		}
	}
}
