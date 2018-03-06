using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseuiNight : MonoBehaviour {

	public GameObject pauseUI;
	public static bool GameIsPaused = false;

	// Use this for initialization
    void Start(){		
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (GameIsPaused) {
				Resume ();
			} else {
				Pause ();
			}
		}
        if (GameControl.instance.gameOver)
        {
            pauseUI.SetActive (true);
            Time.timeScale = 0f;
        }
        if (GameIsPaused) {
            Pause();
        }
	}


	public void Resume(){
        pauseUI.SetActive (false);
        if (GameControl.instance.gameOver){
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
        //GameControl.instance.globalTimer = 0;
        GameControl.instance.gameOver = false;
        GameIsPaused = false;
        pauseUI.SetActive (false);
        SceneControl.Instance.HP = 3;
        SceneControl.Instance.score = 0;
        SceneControl.Instance.LoadScene2();
	}

	public void ExitGame(){
		Debug.Log ("exit game");
        Application.Quit ();
	}

}
