using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class gamebegin : MonoBehaviour {
    public AudioSource button;

    // Use this for initialization
    void Start () {
        this.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            button.Play(); SceneControl.Instance.nexttype = 1;
            StartCoroutine(FadeScene());
        });
	}
    IEnumerator FadeScene()
    {
        float time = GameObject.Find("Fade").GetComponent<FadeScene>().BeginFade(1);
        yield return new WaitForSeconds(time);
        SceneControl.Instance.ani();
    }
    // Update is called once per frame
    void Update () {

    }
}
