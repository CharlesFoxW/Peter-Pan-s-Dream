using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class logocontrol : MonoBehaviour {
	private float tg = 0.0f;
	private float tb = 2.0f;
	// Use this for initialization
	void Start () {
		tg = 0.0f;
	}

	// Update is called once per frame
	void Update()
	{
		tg += Time.deltaTime;
		if (tg >= tb)
		{
			StartCoroutine(FadeScene());
		}
	}
	IEnumerator FadeScene()
	{
		float time = GameObject.Find("Fade").GetComponent<FadeScene>().BeginFade(1);
		yield return new WaitForSeconds(time);
		SceneControl.Instance.Start1();
	}
}
