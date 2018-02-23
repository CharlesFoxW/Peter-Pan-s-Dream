using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickControl : MonoBehaviour {

    private Rigidbody2D rb2d;
    public GameObject brick;
    private Animator anim;

	public float FALL_INTERVAL = 1f;
	private readonly int FRAME_X_LEFT = -12;
	private readonly int FRAME_X_RIGHT = 8;
    private float timeElaspe = 0f;
    private bool shouldTrigger = true;

    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = transform.Find("tower").gameObject.GetComponent<Animator>();

        rb2d.velocity = new Vector2 (GameControl.instance.scrollSpeed, 0);
    }

    void Update() {
		if (transform.position.x < FRAME_X_RIGHT && transform.position.x > FRAME_X_LEFT) {
            if (shouldTrigger) {
                anim.SetTrigger("Shrink");
                shouldTrigger = false;
            } timeElaspe += Time.deltaTime;
            if (timeElaspe >= FALL_INTERVAL && !GameControl.instance.gameOver) {
                Instantiate(brick, new Vector2(transform.position.x,2), Quaternion.identity);
                timeElaspe = 0f;
            }
		} else if (transform.position.x < FRAME_X_LEFT) {
            if (!shouldTrigger) {
                anim.SetTrigger("Still");
                shouldTrigger = true;
            }
        } if (GameControl.instance.gameOver) {
            rb2d.velocity = Vector2.zero;
        }
    }
}
