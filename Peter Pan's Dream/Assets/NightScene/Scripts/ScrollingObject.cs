using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour 
{
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () 
	{
		//Get and store a reference to the Rigidbody2D attached to this GameObject.
		rb2d = GetComponent<Rigidbody2D>();

		//Start the object moving.
		//rb2d.velocity = new Vector2 (GameControl.instance.scrollSpeed, 0);
	}

	void Update()
    {
        // If the game is over, stop scrolling.
        if (!GameControl.instance.gameOver && transform.position.x > GameControl.instance.Boundary_LEFT &&
            transform.position.x < GameControl.instance.Boundary_RIGHT)
        {
            //rb2d.velocity = Vector2.zero;
            transform.position = new Vector2(transform.position.x + GameControl.instance.scrollSpeed * Time.deltaTime, 
                transform.position.y);
        }
    }
}
