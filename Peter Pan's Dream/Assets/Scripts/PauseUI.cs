using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour {

	public GameObject pauseUI;
	public static bool GameIsPaused = false;
	public static bool GameIsOver = false;

	private GameManager gameManager;

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
			pauseUI.SetActive (true);
			Daytimecontrol.timepast = 0f;
			Time.timeScale = 0f;
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
		Time.timeScale = 0f;
		GameIsPaused = true;
	}

	public void Restart(){
		Debug.Log ("restart game.....");
		Time.timeScale = 1f;
		Daytimecontrol.timepast = 0f;
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
