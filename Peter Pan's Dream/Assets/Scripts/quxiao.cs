using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class quxiao : MonoBehaviour {

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Button>().onClick.AddListener(delegate ()
        { 
            StartCoroutine(FadeScene());
        });
    }
    IEnumerator FadeScene()
    {
        float time = GameObject.Find("Fade").GetComponent<FadeScene>().BeginFade(1);
        yield return new WaitForSeconds(time);
        SceneControl.Instance.nexttype = 0;
        SceneManager.LoadScene("Daytime");
    }
}
