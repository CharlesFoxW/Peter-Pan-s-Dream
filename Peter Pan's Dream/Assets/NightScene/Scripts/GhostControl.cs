using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostControl : MonoBehaviour {

    private GameObject target;
    private Vector3 targetPosition;
    private bool isStatic;
    private bool readyToShoot;
    private readonly float ghostShootSpeed = 12f;
    private float holdingTime;
    private Rigidbody2D rb2d;
    private Animator anim;

	// Use this for initialization
    void Start () {
        target = GameControl.instance.Player;
        isStatic = false;
        readyToShoot = false;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x < 5f && transform.position.x > 3f) {
            if (!isStatic && !readyToShoot) {
                isStatic = true;
                rb2d.velocity = new Vector2(0f, 0f);
                holdingTime = 0f;
            } else if (isStatic && !readyToShoot) {
                if (holdingTime < 1f) {
                    holdingTime += Time.deltaTime;
                } else {
                    targetPosition = target.transform.position;
                    readyToShoot = true;
                }
            }
        }

        if (isStatic && readyToShoot) {
            anim.SetTrigger("Stop");
            float step = ghostShootSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        }

        if (transform.position.Equals(targetPosition)) {
            transform.gameObject.SetActive(false);
            rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed, 0);
            readyToShoot = false;
            isStatic = false;
        }

        if (transform.position.x > 12f) {
            transform.gameObject.SetActive(true);
            anim.SetTrigger("Start");
            rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed, 0);
        }
	}
}
