using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainButtonPlay : MonoBehaviour {

    public GameObject myLoading;
    public GameObject myFade;
	// Use this for initialization
	void Start () {
        this.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            StartCoroutine(FadeScene());
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator FadeScene()
    {
        float time = myFade.GetComponent<FadeScene>().BeginFade(1);
        yield return new WaitForSeconds(time);
        myLoading.gameObject.SetActive(true);
    }
}
