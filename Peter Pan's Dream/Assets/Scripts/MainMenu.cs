using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public Text scoreText = null;
	private bool isShowScore = false;

	public void PlayGame(){
		Debug.Log ("play");
	}
		

	public void QuitGame(){
		Debug.Log ("QUIT!");
		Application.Quit ();
	}

	public void HighestScore(){
		Debug.Log ("HigestScore");
	
		if (isShowScore){
			scoreText.text = "Highest Scores";
			isShowScore = false;
			return;
		}

		int higestscores = 0;
		if (PlayerPrefs.HasKey ("HighestScores")) {
			higestscores = PlayerPrefs.GetInt ("HighestScores");
		} else {
			PlayerPrefs.SetInt ("HighestScores", higestscores);
		}
		if (!isShowScore) {
			scoreText.text = higestscores.ToString ();
			isShowScore = true;
		}
	}
}
