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

	private float lastHeight;

	public Transform lowerPoint;
	public Transform middlePoint;
	public Transform highPoint;
	public Transform maxHeightPoint;

	private float lowerHeight;
	private float middleHeight;
	private float highHeight;
	private float maxHeight;
	private float offset;

	//	public float maxHeightChange;
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

		lowerHeight = lowerPoint.position.y;
		middleHeight = middlePoint.position.y;
		highHeight = highPoint.position.y;
		maxHeight = maxHeightPoint.position.y;
		offset = 1.0f;
		lastHeight = lowerHeight;

		theCoinGenerator = FindObjectOfType<CoinGenerator> ();
	}

	// Update is called once per frame
	void Update () {


		if (transform.position.x < generationPoint.position.x){
			distanceBetween = Random.Range (distanceBetweenMin, distanceBetweenMax);


			//add thick platform
			float curHeight = 0;

			if (lastHeight <= middleHeight){ //lower
				platformSelector = Random.Range (0, 7);
			}else if (lastHeight <= highHeight){ //middle
				platformSelector = Random.Range (0, theObjectPools.Length);
			}else {//high
				platformSelector = Random.Range (3, 7); 
			}


			if (platformSelector < 3) {
				curHeight = Random.Range (lowerHeight, middleHeight - offset);
			} else if (platformSelector < 7) { 
				curHeight = Random.Range (middleHeight, highHeight - 2.0f * offset);
			} else { 
				curHeight = Random.Range (highHeight, maxHeight);
			}	

			lastHeight = curHeight;

			transform.position = new Vector3 (transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween,
				curHeight, transform.position.z);


			GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();
			newPlatform.transform.position = transform.position;
			newPlatform.transform.rotation = transform.rotation;
			newPlatform.SetActive (true);

			if (Random.Range (0f, 100f) < randomCoinThreshold) {
				theCoinGenerator.SpawnCoins (new Vector3 (transform.position.x,
					transform.position.y + 1f, transform.position.z), platformWidths[platformSelector]);
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
