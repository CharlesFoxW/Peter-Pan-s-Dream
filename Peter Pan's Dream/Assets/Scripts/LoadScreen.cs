using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScreen : MonoBehaviour {

	public Slider slider;

	AsyncOperation async;



	// Use this for initialization
	void Start () {

		StartCoroutine (LoadingScreen());
	}

	IEnumerator LoadingScreen (){
		async = SceneManager.LoadSceneAsync (SceneManager.GetActiveScene().buildIndex + 1);
		async.allowSceneActivation = false;

		while (async.isDone == false){
			slider.value = async.progress;
			if (async.progress == 0.9f){
				slider.value = 1f;
				async.allowSceneActivation = true;
			}
			yield return null;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
