using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseUI : MonoBehaviour {

	public GameObject pauseUI;
	public static bool GameIsPaused = false;
	public static bool GameIsOver = false;

	private GameManager gameManager;

	public Text scoreinfo;

	// Use this for initialization
	void Start(){
		gameManager = FindObjectOfType<GameManager> ();
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (GameIsPaused) {
				Resume ();
			} else {
				Pause ();
			}
		}

        if (GameIsPaused) {
            Pause();
        }

        if (GameIsOver){
			gameOver ();
		}
	}


	public void Resume(){
		pauseUI.SetActive (false);
		if (GameIsOver){
			Restart ();
		}else{
			Time.timeScale = 1f;
			GameIsPaused = false;
		}
	}

	void Pause(){
		pauseUI.SetActive (true);
		scoreinfo.text = "";
		Time.timeScale = 0f;
		GameIsPaused = true;
	}


	public void gameOver(){
		pauseUI.SetActive (true);
		int highestscore = PlayerPrefs.GetInt ("HighestScores");
		if (SceneControl.Instance.score >= highestscore){
			PlayerPrefs.SetInt ("HighestScores", SceneControl.Instance.score);
			scoreinfo.text = "CONGRADULATIONS!!!\n YOU GOT THE HIGHEST SCORES " + SceneControl.Instance.score; 
		}else{
			scoreinfo.text = "YOU GOT " + SceneControl.Instance.score + "\n" +
				"HIGHEST SCORE : " +  highestscore;
		}
		DaytimeControl.timepast = 0f;
		Time.timeScale = 0f;
	}

	public void Restart(){
		Debug.Log ("restart game.....");
		Time.timeScale = 1f;
		DaytimeControl.timepast = 0f;
		GameIsOver = false;
        GameIsPaused = false;
		pauseUI.SetActive (false);
		gameManager.RestartGame ();
	}

	public void ExitGame(){
		Debug.Log ("exit game");
		Application.Quit ();
	}
		
}
