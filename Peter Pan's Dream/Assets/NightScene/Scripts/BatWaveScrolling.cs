using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatWaveScrolling : MonoBehaviour 
{
	private Rigidbody2D rb2d;
    private float waveSpeed;

	// Use this for initialization
	void Start () 
	{
		//Get and store a reference to the Rigidbody2D attached to this GameObject.
		rb2d = GetComponent<Rigidbody2D>();

		//Start the object moving.
		waveSpeed = GameControl.instance.scrollSpeed * 2f;
		//rb2d.velocity = new Vector2 (waveSpeed, 0);
	}

	void Update()
	{
		// If the game is over, stop scrolling.
        if (!GameControl.instance.gameOver && transform.position.x > GameControl.instance.Boundary_LEFT)
		{
            transform.position = new Vector2(transform.position.x + waveSpeed * Time.deltaTime, transform.position.y);
		}
        else
        {
            DestroyGameObject();
        }
	}

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}

