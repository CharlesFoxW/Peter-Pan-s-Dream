using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class quit : MonoBehaviour {
    public AudioSource button;

    // Use this for initialization
    public void Start() {
        this.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            button.Play();
            StartCoroutine(FadeScene());
        });
    }
    private string Quit()
    {
        Application.Quit();
        return "";
    }
    IEnumerator FadeScene()
    {
        float time = GameObject.Find("Fade").GetComponent<FadeScene>().BeginFade(1);
        yield return new WaitForSeconds(time);
        Quit();
    }
    // Update is called once per frame
    public void Update () {
    }
}
