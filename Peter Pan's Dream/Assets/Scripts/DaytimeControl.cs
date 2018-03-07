using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaytimeControl : MonoBehaviour {

    public GameObject myFade;
    public static float timepast = 0f;
    private float timebar = 10f;

    private ScoreManager myScoreManager;

    // Use this for initialization
    void Start()
    {
        timepast = 0f;
        myScoreManager = FindObjectOfType<ScoreManager>();
    }
	
    // Update is called once per frame
    void Update()
    {
        timepast += Time.deltaTime;
        if (!PlayerController.isDead && timepast >= timebar)
        {
            Debug.Log("inchange");
            timepast = 0f;
            SceneControl.Instance.score = myScoreManager.getScore();
            StartCoroutine(FadeScene());
        }
    }

    IEnumerator FadeScene()
    {
        Time.timeScale = 0.5f;
        float time = myFade.GetComponent<FadeScene>().BeginFade(1);
        yield return new WaitForSeconds(time);
        Time.timeScale = 1f;
        SceneControl.Instance.LoadScene1();
    }
}
