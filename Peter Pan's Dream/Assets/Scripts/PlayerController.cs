﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpForce;
	public GameManager gameManager;

	//public float jumpTime;
	//private float jumpTimeCounter;

	private bool isJumpOnce; 
	private bool isJumpTwice;

	private Rigidbody2D myRigidbody;
	private Animator myAnimator;
	private Collider2D myCollider;

	public AudioSource audioJump;
	public AudioSource audioHurt;
	public AudioSource audioDie;
	public AudioSource audioBg;
	public AudioSource audioHealUp;


	private float moveSpeedStore;

    public static bool isDead = false;

	// Use this for initialization
	void Start () {
        PlayerController.isDead = false;
		myRigidbody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
		moveSpeedStore = moveSpeed;
	}

    // Update is called once per frame
    void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y); // keep running


        // jump movement
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // only hit jump twice
            if (!isJumpOnce)
            {
                addJumpForce(jumpForce);
                isJumpOnce = true;
                audioJump.Play();
            }
            else if (!isJumpTwice)
            {
                addJumpForce(jumpForce);
                isJumpTwice = true;
                audioJump.Play();
            }
        }

        // Touch Screen Control:
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // only hit jump twice
                if (!isJumpOnce)
                {
                    addJumpForce(jumpForce);
                    isJumpOnce = true;
                    audioJump.Play();
                }
                else if (!isJumpTwice)
                {
                    addJumpForce(jumpForce);
                    isJumpTwice = true;
                    audioJump.Play();
                }
            }


        }


	}

	private void addJumpForce (float upForce){
		myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, upForce);
		myAnimator.SetBool ("Grounded",false);
	}

	IEnumerator OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) 
		{
			if (collision.relativeVelocity.y > 0) {
				isJumpOnce = false;
				isJumpTwice = false;
			}
			myAnimator.SetBool ("Grounded", true);

		}

		if (collision.collider.gameObject.tag == "killbox" ){
			Debug.Log ("die1");
            PlayerController.isDead = true;
			audioDie.Play ();
			audioBg.Stop ();
			moveSpeed = 0;
			isJumpOnce = true;
			isJumpTwice = true;
			myRigidbody.velocity = new Vector2 (0, 0);

			yield return new WaitForSeconds (2.5f);
			//audioBg.Play ();

			PauseUI.GameIsOver = true;
			//gameManager.RestartGame ();	
			// initalize parameters while restarting the game
			moveSpeed = moveSpeedStore;
		}
			
			
	}

	IEnumerator OnTriggerEnter2D(Collider2D other){
		if (other.tag == "DTEnemy") {
			
			SceneControl.Instance.HP--;

			if (SceneControl.Instance.HP > 0) {
				audioHurt.Play ();

			} else {
				Debug.Log ("die2");
                PlayerController.isDead = true;
				audioDie.Play ();
				audioBg.Stop ();
				moveSpeed = 0;
				isJumpOnce = true;
				isJumpTwice = true;
				myRigidbody.velocity = new Vector2 (0, 0);
				Vector3 prevScale = transform.localScale;
				transform.localScale = new Vector3 (0, 0, 0);

				//gameObject.SetActive (false); // stuck if uncomment this line

				yield return new WaitForSeconds (2.5f);
				transform.localScale = prevScale;

				//audioBg.Play ();

				PauseUI.GameIsOver = true;
				//gameManager.RestartGame ();	
				// initalize parameters while restarting the game
				moveSpeed = moveSpeedStore;
			}
        } else if (other.tag == "Heal") {
            if (SceneControl.Instance.HP < 3 && SceneControl.Instance.HP > 0) {
                Debug.Log("Healing by 1...");
                SceneControl.Instance.HP++;
            }
			audioHealUp.Play ();
            other.gameObject.SetActive(false);
        }

	}


}
