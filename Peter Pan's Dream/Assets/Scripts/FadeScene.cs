using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScene : MonoBehaviour {
    
    public Texture blackTexture;
    public float fadeSpeed;

    private float alpha = 1.0f;
    private int fadeDir = -1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI()
    {
        if ((fadeDir > 0 && alpha <= 1f) || (fadeDir < 0 && alpha >= 0f))
        {
            alpha += fadeDir * fadeSpeed * Time.deltaTime;
        }
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blackTexture);
    }
    public float BeginFade(int direction)
    {
        fadeDir = direction;
        // Adjusted the Divided Value to Adapt the Timing Issue.
        return 0.5f / fadeSpeed;
    }

    void onLevelwasLoading()
    {
        BeginFade(-1);
    }
}
