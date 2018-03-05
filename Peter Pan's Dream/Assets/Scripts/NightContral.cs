using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NightContral : MonoBehaviour {

	// Use this for initialization
	void Start () {
        timepast = 0f;
	}
	private float timepast=0f;
    private float timebar = 5f;
	// Update is called once per frame
	void Update () {
        timepast += Time.deltaTime;
        if (timepast >= timebar)
        {
			StartCoroutine(FadeScene());
        }
	}

	IEnumerator FadeScene() {
		float time = GameObject.Find ("Fade").GetComponent<FadeScene> ().BeginFade (1);
		yield return new WaitForSeconds (time);
		SceneControl.Instance.LoadScene1 ();
	}
}
