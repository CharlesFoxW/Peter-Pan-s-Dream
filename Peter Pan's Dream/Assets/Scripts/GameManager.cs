using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Transform platformGenerator;
	private Vector3 platformStartPoint;

	public PlayerController thePlayer;
	private Vector3 playerStartPoint;

	private Vector3 backGroundPosition;

	private PlatformDestroyer[] platformList;

	private ScoreManager theScoreManager;


	// Use this for initialization
	void Start () {
		platformStartPoint = platformGenerator.position;
		playerStartPoint = thePlayer.transform.position;
		backGroundPosition = FindObjectOfType<ScrollingBackground> ().gameObject.transform.position;
		theScoreManager = FindObjectOfType<ScoreManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RestartGame(){
		StartCoroutine ("RestartGameCo");
	}

	public IEnumerator RestartGameCo(){
		theScoreManager.scoreIncreasing = false;
		thePlayer.gameObject.SetActive (false);
		yield return new WaitForSeconds (0.5f);

		//destroy all objects remaining 
		platformList = FindObjectsOfType<PlatformDestroyer> ();
		for (int i = 0; i < platformList.Length; i++){
			platformList [i].gameObject.SetActive (false);
		}


		thePlayer.transform.position = playerStartPoint;
		platformGenerator.position = platformStartPoint;

		//Set background position
		ScrollingBackground[] backgrounds = FindObjectsOfType<ScrollingBackground>();
			for (int i = 0; i < backgrounds.Length; i++){
				backgrounds [i].gameObject.transform.position = backGroundPosition;		
		}
			

		thePlayer.gameObject.SetActive (true);
        PlayerController.isDead = false;
		SceneControl.Instance.HP = 3;
		theScoreManager.scoreCount = 0;
		theScoreManager.scoreIncreasing = true;

	}
}
