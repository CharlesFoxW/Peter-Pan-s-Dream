using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyWithInvincibleControl : MonoBehaviour {

    private Rigidbody2D rb2d;
    private float Max_Y = 2.2f;
    private float Min_Y = -1.2f;
    private float cur_Y;
    private float duration = 1.5f;
    private float timeElapsed = 0f;
    private float pTime = 0f;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        cur_Y = Max_Y;
        //rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x < GameControl.instance.Boundary_RIGHT 
            && transform.position.x > GameControl.instance.Boundary_LEFT) {
//            transform.position = new Vector2 (transform.position.x + GameControl.instance.scrollSpeed * Time.deltaTime, 
//                Mathf.Sin (Time.time * 2) * 2);
            transform.position = new Vector2 (transform.position.x + GameControl.instance.scrollSpeed * Time.deltaTime, 
                cur_Y);
            timeElapsed += Time.deltaTime;
            if (timeElapsed - pTime > duration)
            {
                pTime = timeElapsed;
                if (cur_Y == Max_Y)
                    cur_Y = Min_Y;
                else
                    cur_Y = Max_Y;
            }
        } else {
            transform.GetChild (0).gameObject.SetActive (true);
            timeElapsed = 0f;
            pTime = 0f;
        }
    }
}
