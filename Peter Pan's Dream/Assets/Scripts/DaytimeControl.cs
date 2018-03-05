using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaytimeControl : MonoBehaviour {

	public GameObject myFade;
    private float timepast = 0f;
    private float timebar = 10f;
    private bool sceneIsChanged;

    // Use this for initialization
    void Start()
    {
        timepast = 0f;
        sceneIsChanged = false;
    }

    // Update is called once per frame
    void Update()
    {
		
		timepast += Time.deltaTime;

		if (!sceneIsChanged && !PlayerController.isDead && timepast >= timebar)
        {
            Debug.Log("inchange");
            sceneIsChanged = true;
            timepast = 0f;
			StartCoroutine (FadeScene ());
        }

    }

	IEnumerator FadeScene() {

		float time = myFade.GetComponent<FadeScene>().BeginFade (1);
		yield return new WaitForSeconds (time);
		SceneControl.Instance.LoadScene1 ();

	}


}
