using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
public class IntroScene : MonoBehaviour {
    public AudioSource button;
    void Start () {
        this.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            button.Play();
            StartCoroutine(FadeScene());
        });
    }
    IEnumerator FadeScene()
    {
        float time = GameObject.Find("Fade").GetComponent<FadeScene>().BeginFade(1);
        yield return new WaitForSeconds(time);
        SceneControl.Instance.LoadScene3();
    }
    // Update is called once per frame
    void Update () {
    }
}
