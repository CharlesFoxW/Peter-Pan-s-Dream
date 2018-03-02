using System.Collections;
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
	public AudioSource audioDie;
	public AudioSource audioBg;

	private float moveSpeedStore;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
		moveSpeedStore = moveSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		myRigidbody.velocity = new Vector2 (moveSpeed, myRigidbody.velocity.y); // keep running


		// jump movement
		if (Input.GetKeyDown(KeyCode.UpArrow)){
			// only hit jump twice
			if (!isJumpOnce) 
			{
				addJumpForce (jumpForce);
				isJumpOnce = true;
				audioJump.Play ();
			} else if (!isJumpTwice)
			{
				addJumpForce (jumpForce);
				isJumpTwice = true;
				audioJump.Play ();
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
			Debug.Log (collision.relativeVelocity.y);
			if (collision.relativeVelocity.y > 0) {
				isJumpOnce = false;
				isJumpTwice = false;
			}
			myAnimator.SetBool ("Grounded", true);

		}

		if (collision.collider.gameObject.tag == "killbox" ){
			audioDie.Play ();
			audioBg.Stop ();
			moveSpeed = 0;
			isJumpOnce = true;
			isJumpTwice = true;
			myRigidbody.velocity = new Vector2 (0, 0);

			yield return new WaitForSeconds (2.5f);
			audioBg.Play ();
			gameManager.RestartGame ();	
			// initalize parameters while restarting the game
			moveSpeed = moveSpeedStore;
		}
			
			
	}

	IEnumerator OnTriggerEnter2D(Collider2D other){
		if (other.tag == "DTEnemy") {
			if (SceneControl.Instance.HP > 1) {
				SceneControl.Instance.HP--;
			} else {
				audioDie.Play ();
				audioBg.Stop ();
				moveSpeed = 0;
				isJumpOnce = true;
				isJumpTwice = true;
				gameObject.SetActive (false);

				yield return new WaitForSeconds (2.5f);
				audioBg.Play ();
				gameManager.RestartGame ();	
				// initalize parameters while restarting the game
				moveSpeed = moveSpeedStore;
			}
		}

	}


}
