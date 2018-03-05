using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daytimecontrol : MonoBehaviour {

	public GameObject myFade;

    // Use this for initialization
    void Start()
    {
        timepast = 0f;
    }
    private float timepast = 0f;
    private float timebar = 15f;
    // Update is called once per frame
    void Update()
    {
		
		timepast += Time.deltaTime;

		if (!PlayerController.isDead && timepast >= timebar)
        {
            Debug.Log("inchange");
            timepast = 0f;
			StartCoroutine (FadeScene ());
        }

    }

	IEnumerator FadeScene() {

		float time = myFade.GetComponent<FadeScene> ().BeginFade (1);
		yield return new WaitForSeconds (time);
		SceneControl.Instance.LoadScene1 ();

	}


}
