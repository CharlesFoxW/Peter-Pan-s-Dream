using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickControl : MonoBehaviour {

    private Rigidbody2D rb2d;
    public GameObject brick;
    private Animator anim;

	public float FALL_INTERVAL = 1f;
    private float timeElaspe = 0f;

    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = transform.Find("tower").gameObject.GetComponent<Animator>();

        //rb2d.velocity = new Vector2 (GameControl.instance.scrollSpeed, 0);
    }

    void Update() {
        if (!GameControl.instance.gameOver && transform.position.x < GameControl.instance.Boundary_RIGHT 
            && transform.position.x > GameControl.instance.Boundary_LEFT) {
            transform.position = new Vector2(transform.position.x + GameControl.instance.scrollSpeed * Time.deltaTime, 
                transform.position.y);
            timeElaspe += Time.deltaTime;
            anim.SetTrigger("Shrink");

            if (timeElaspe >= FALL_INTERVAL) {
                Instantiate(brick, new Vector2(transform.position.x,2), Quaternion.identity);
                timeElaspe = 0f;
            }

		} else
        {
            anim.SetTrigger("Still");
        }
    }
}
