using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text scoreText;
	public Text highScoreText;

	public Image starHP1;
	public Image starHP2;
	public Image starHP3;

	public float scoreCount;
	public float hiscoreCount; 

	public float pointsPerSecond;

	public bool scoreIncreasing;

	// Use this for initialization
	void Start () {
        scoreCount += SceneControl.Instance.score;
	}
	
	// Update is called once per frame
	void Update () {

        //SceneControl.Instance.score = (int)Mathf.Round(scoreCount);

        if (!PlayerController.isDead && scoreIncreasing){
            scoreCount += (pointsPerSecond * Time.deltaTime);
		}

//		if (scoreCount > hiscoreCount){
//			hiscoreCount = scoreCount;
//			PlayerPrefs.SetFloat ("HighScore", hiscoreCount);
//		}

		SceneControl.Instance.score = (int)Mathf.Round (scoreCount);
		scoreText.text = "Score : " + SceneControl.Instance.score;
		//hp.text = "HP : " + SceneControl.Instance.HP;
		//highScoreText.text = "High Score : " + Mathf.Round (hiscoreCount);

		// Update the HP stars:
		if (SceneControl.Instance.HP >= 3) {
			starHP1.gameObject.SetActive (true);
			starHP2.gameObject.SetActive (true);
			starHP3.gameObject.SetActive (true);
		} else if (SceneControl.Instance.HP == 2) {
			starHP1.gameObject.SetActive (true);
			starHP2.gameObject.SetActive (true);
			starHP3.gameObject.SetActive (false);
		} else if (SceneControl.Instance.HP == 1) {
			starHP1.gameObject.SetActive (true);
			starHP2.gameObject.SetActive (false);
			starHP3.gameObject.SetActive (false);
		} else {
			starHP1.gameObject.SetActive (false);
			starHP2.gameObject.SetActive (false);
			starHP3.gameObject.SetActive (false);
		}
	}

	public void AddScore(int pointsToAdd){
        scoreCount += pointsToAdd;
	}


}
