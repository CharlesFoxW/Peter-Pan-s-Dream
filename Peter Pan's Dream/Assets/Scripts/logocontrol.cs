using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class logocontrol : MonoBehaviour {
	private float tg = 0.0f;
	private float tb =4f;
	// Use this for initialization
	void Start () {
		tg = 0.0f;
	}

	// Update is called once per framea
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
