using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animcontrol : MonoBehaviour {
    private float time = 6.5f;
    private float nowtime = 0.0f;
    // Use this for initialization
    void Start () {
        nowtime = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        nowtime += Time.deltaTime;
        if (nowtime>=time)
        {
            nowtime = 0f;
            switch (SceneControl.Instance.nexttype)
            {
                case 0:
                    StartCoroutine(FadeScene2());
                    break;
                case 1:
                    StartCoroutine(FadeScene1());
                    break;
                case 2:
                    StartCoroutine(FadeScene3());
                    break;
            }
            
        }
    }
    IEnumerator FadeScene1()
    {
        float time = GameObject.Find("Fade").GetComponent<FadeScene>().BeginFade(1);
        yield return new WaitForSeconds(time);
        SceneControl.Instance.daybegin();
    }
    IEnumerator FadeScene2()
    {
        float time = GameObject.Find("Fade").GetComponent<FadeScene>().BeginFade(1);
        yield return new WaitForSeconds(time);
        SceneControl.Instance.Start1();

    }
    IEnumerator FadeScene3()
    {
        float time = GameObject.Find("Fade").GetComponent<FadeScene>().BeginFade(1);
        yield return new WaitForSeconds(time);
        SceneControl.Instance.LoadScene1();
    }
}
