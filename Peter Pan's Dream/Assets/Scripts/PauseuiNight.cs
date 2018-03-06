using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseuiNight : MonoBehaviour {

	public GameObject pauseUI;
	public static bool GameIsPaused = false;

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

	}


	public void Resume(){
		pauseUI.SetActive (false);
		Time.timeScale = 1f;
		GameIsPaused = false;

	}

	void Pause(){
		pauseUI.SetActive (true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}

	public void Restart(){
		Debug.Log ("restart game.....");
	}

	public void ExitGame(){
		Debug.Log ("exit game");
	}

}
