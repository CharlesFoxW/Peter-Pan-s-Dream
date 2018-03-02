using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadyBugController : MonoBehaviour {

	private GameManager myGameManager;
	private Rigidbody2D myRigidbody;

	private Vector3 platformPosition;
	private float platformSize = 0;
	private Vector3 rightCorner;
	private Vector3 leftCorner;

	private bool movingRight = false;
	public float movingSpeed;

	private PlayerController theController;

	public static LadyBugController PassParameters(GameObject where, Vector3 pfPosition, float pfSize) {
		LadyBugController myLadyBugController = where.GetComponent<LadyBugController>();
		myLadyBugController.platformPosition = pfPosition;
		myLadyBugController.platformSize = pfSize;
		myLadyBugController.rightCorner = new Vector3 (pfPosition.x + 100f, pfPosition.y + 1f, pfPosition.z);
		myLadyBugController.leftCorner = new Vector3 (pfPosition.x - 100f, pfPosition.y + 1f, pfPosition.z);
		return myLadyBugController;
	}

	// Use this for initialization
	void Start () {
		theController = FindObjectOfType<PlayerController> ();
		myRigidbody = theController.GetComponent<Rigidbody2D> ();
		myGameManager = FindObjectOfType<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {

		// Turning the Head
		if (movingRight && transform.position.x > platformPosition.x + (platformSize / 2)) {
			movingRight = false;
			transform.rotation = Quaternion.AngleAxis (0, Vector3.up);
		}
		if (!movingRight && transform.position.x < platformPosition.x - (platformSize / 2)) {
			movingRight = true;
			transform.rotation = Quaternion.AngleAxis (180, Vector3.up);
		}
		// Move	
		if (movingRight) {
			transform.position = Vector3.MoveTowards (transform.position, rightCorner, movingSpeed * Time.deltaTime);
		} else {
			transform.position = Vector3.MoveTowards (transform.position, leftCorner, movingSpeed * Time.deltaTime);
		}
			
	}

	IEnumerator OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Player"){
			Debug.Log ("HIIIII");
			if (SceneControl.Instance.HP > 1) {
				SceneControl.Instance.HP--;
			} else {
				theController.audioDie.Play ();
				theController.audioBg.Stop ();

				myRigidbody.velocity = new Vector2 (0, 0);

				yield return new WaitForSeconds (1f);
				theController.audioBg.Play ();
				myGameManager.RestartGame ();	
				// initalize parameters while restarting the game

			}
		}
	}
		
}
