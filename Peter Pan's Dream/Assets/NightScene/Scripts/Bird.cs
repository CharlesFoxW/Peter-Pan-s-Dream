﻿using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour 
{
	public float upForce;					//Upward force of the "flap".
    private float protectTime = 2f;
    public float invincibleTime = 10f;
	private bool isDead = false;			//Has the player collided with a wall?
    private float timeElapse = 0f;
    private float protectTimeElapse = 0f;
    private float invincibleTimeElapse = 0f;
    private bool isCollided = false;
    private bool isInvincible = false;
	//private Animator anim;					//Reference to the Animator component.
	private Rigidbody2D rb2d;				//Holds a reference to the Rigidbody2D component of the bird.
    private Collider2D polycollider;
    private Animator anim;
    private float moveSpeed = 0.1f;
    private float currentScrollSpeed;

	public AudioSource asCoin;
    public AudioSource hurtsound;
    public AudioSource diesound;
    public AudioSource healsound;

	void Start(){
		//Get reference to the Animator component attached to this GameObject.
		//anim = GetComponent<Animator> ();
		//Get and store a reference to the Rigidbody2D attached to this GameObject.
		rb2d = GetComponent<Rigidbody2D>();
        polycollider = GetComponent<PolygonCollider2D>();
        anim = GetComponent<Animator>();
        polycollider.isTrigger = true;
	}

	void Update()
    {
        if (isCollided)
        {
            protectTimeElapse += Time.deltaTime;
            if (protectTimeElapse >= protectTime)
            {
                anim.SetTrigger("idle");
                isCollided = false;
                protectTimeElapse = 0f;
            }
        }
        if (isInvincible)
        {
            invincibleTimeElapse += Time.deltaTime;
            if (invincibleTimeElapse >= invincibleTime)
            {
                anim.SetTrigger("idle");
                isInvincible = false;
                invincibleTimeElapse = 0f;
                GameControl.instance.scrollSpeed = currentScrollSpeed;
            }
        }
        //Don't allow control if the bird has died.
        if (isDead == false) 
		{
            if (Input.GetKey(KeyCode.UpArrow)) 
            {
                //rb2d.AddForce(new Vector2(0, upForce));
                transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                //rb2d.AddForce(new Vector2(0, -upForce));
                transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed);
            }


            timeElapse += Time.deltaTime;
            if (timeElapse >= GameControl.instance.scoreRate)
            {
                GameControl.instance.BirdScored(1);
                timeElapse = 0;
            }
		}
	}

    void OnTriggerEnter2D(Collider2D other) {
		if (!isCollided && !isInvincible && other.tag != "boundary" && other.tag != "star" && other.tag != "Heal" 
            && other.tag != "magnet" && other.tag != "coin" && other.tag != "Invincible"
            && other.tag != "BronzeCoin" && other.tag != "SilverCoin") {
			if (GameControl.instance.ReduceHP (1)) {
                diesound.Play();
				BirdDie ();
			} else {
                hurtsound.Play();
				anim.SetTrigger("collide");
				isCollided = true;
			}
        } else if(other.tag == "boundary") {
			GameControl.instance.ReduceHP (3);
			BirdDie();
        } else if (other.gameObject.CompareTag("star")) {
            other.gameObject.SetActive(false);
            GameControl.instance.BirdScored(5);
            if (GameControl.instance.updateStars == false) {
                GameControl.instance.RenewStars(other.gameObject.transform.parent);
            }
		} else if (other.gameObject.CompareTag("Heal")) {
			GameObject heal = other.gameObject.transform.GetChild (0).gameObject;
			if (heal.activeSelf) {
				heal.SetActive(false);
				GameControl.instance.IncreaseHP (1);
                healsound.Play();
			}
		} else if (other.gameObject.CompareTag("magnet")) {
            other.gameObject.SetActive(false);
            GameControl.instance.hasMagnet = true;
            GameControl.instance.fairyWithMag = other.gameObject.transform.parent;
        } else if (other.gameObject.CompareTag("coin")) {
            GameControl.instance.BirdScored(5);
			asCoin.Play ();
            other.gameObject.SetActive(false);

        } else if (other.gameObject.CompareTag("BronzeCoin")) {
            GameControl.instance.BirdScored(1);
            asCoin.Play ();
            other.gameObject.SetActive(false);

        } else if (other.gameObject.CompareTag("SilverCoin")) {
            GameControl.instance.BirdScored(2);
            asCoin.Play ();
            other.gameObject.SetActive(false);

        } else if (other.gameObject.CompareTag("Invincible")) {
            GameObject invincible = other.gameObject.transform.GetChild(0).gameObject;
            if (invincible.activeSelf)
            {
                currentScrollSpeed = GameControl.instance.scrollSpeed;
                GameControl.instance.scrollSpeed = -15f; //Speedup the scene
                invincible.SetActive(false);
                anim.SetTrigger("collide");
                isCollided = false;
                isInvincible = true;
            }
        }

    }

    void BirdDie() {
        polycollider.isTrigger = false;
        rb2d.velocity = Vector2.zero;
        rb2d.gravityScale = 1f;
        isDead = true;
        GameControl.instance.BirdDied();
    }
}
