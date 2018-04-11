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
	private SpriteRenderer myRenderer;
	private ScoreManager myScoreManager;
	public GameObject myFade;

	public AudioSource audioJump;
	public AudioSource audioHurt;
	public AudioSource audioDie;
	public AudioSource audioBg;
	public AudioSource audioHealUp;

	public static float PlayerAlpha = 0f;
	public static int PlayerFadeDir = 1;
	private float fadeSpeed = 0.6f;
	private bool isChangingScene = false;

	private float moveSpeedStore;

    public static bool isDead = false;
    private bool isInvincible = false;
    private float invincibleTimeElapse = 0f;
    public float invincibleTime = 2.5f;
	private float playerBlinkingElapse = 0f;
	private float playerBlinkingMiniDuration = 0.2f;

	// Use this for initialization
	void Start () {
        PlayerController.isDead = false;
		PlayerAlpha = 0f;
		PlayerFadeDir = 1;
		myRigidbody = GetComponent<Rigidbody2D> ();
		myCollider = GetComponent<Collider2D> ();
		myAnimator = GetComponent<Animator> ();
		myRenderer = GetComponent<SpriteRenderer> ();
		moveSpeedStore = moveSpeed;
		myScoreManager = FindObjectOfType<ScoreManager>();
	}

    // Update is called once per frame
    void Update()
    {
		if (!isChangingScene) {
        	myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y); // keep running
		}


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
		// Blinking Animation:
        if (!isChangingScene && isInvincible) {

			playerBlinkingElapse += Time.deltaTime;
			if (playerBlinkingElapse > playerBlinkingMiniDuration) {
				if (myRenderer.enabled) {
					myRenderer.enabled = false;
				} else {
					myRenderer.enabled = true;
				}
				playerBlinkingElapse = 0f;
			}

            invincibleTimeElapse += Time.deltaTime;
            if (invincibleTimeElapse > invincibleTime) {
                // Add invincible animation here.
				myRenderer.enabled = true;
                isInvincible = false;
                invincibleTimeElapse = 0f;
            	
            }

        }

		if ((PlayerFadeDir > 0 && PlayerAlpha <= 1f) || (PlayerFadeDir < 0 && PlayerAlpha >= 0f)) {
			PlayerAlpha += PlayerFadeDir * fadeSpeed * Time.deltaTime;

			myRenderer.material.color = new Color(myRenderer.material.color.r, 
				myRenderer.material.color.g, myRenderer.material.color.b, PlayerAlpha);
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

		if (!isChangingScene && collision.collider.gameObject.tag == "killbox" ){
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
		if (!isInvincible && other.tag == "DTEnemy") {
			
			SceneControl.Instance.HP--;

			if (SceneControl.Instance.HP > 0) {
				isInvincible = true;
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
				Debug.Log ("Healing by 1...");
				SceneControl.Instance.HP++;
			}
			audioHealUp.Play ();
			other.gameObject.SetActive (false);

		} else if (other.tag == "Blackhole" && !PlayerController.isDead) {
			Debug.Log("inchange");
			SceneControl.Instance.score = myScoreManager.getScore();
			Time.timeScale = 0.5f;
			PlayerFadeDir = -1;
			fadeSpeed = 2.4f;
			isChangingScene = true;
			isInvincible = true;
			myRigidbody.velocity = new Vector2 (1.67f, myRigidbody.velocity.y / 3f);
			float time = myFade.GetComponent<FadeScene>().BeginFade(1);
			yield return new WaitForSeconds(time);
			isChangingScene = false;
			isInvincible = false;
			PlayerFadeDir = 0;
			fadeSpeed = 0.6f;
			Time.timeScale = 1f;
			SceneControl.Instance.LoadScene1();
		}

	}


}
