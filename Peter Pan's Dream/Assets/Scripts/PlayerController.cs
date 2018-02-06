using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpForce;

	private bool isJumpOnce; 
	private bool isJumpTwice;

	private Rigidbody2D myRigidbody;
	private Animator myAnimator;
	private Collider2D myCollider;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
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
			}else if (!isJumpTwice)
			{
				addJumpForce (jumpForce);
				isJumpTwice = true;
			}
		
		}


	}

	private void addJumpForce (float upForce){
		myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, upForce);
		myAnimator.SetBool ("Grounded",false);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		
		if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) 
		{
			isJumpOnce = false;
			isJumpTwice = false;
			myAnimator.SetBool ("Grounded",true);
		}

	}



}
