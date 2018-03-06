using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void PlayGame(){
		Debug.Log ("play");
	}
		

	public void QuitGame(){
		Debug.Log ("QUIT!");
		Application.Quit ();
	}
}
