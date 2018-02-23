using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatWaveScrolling : MonoBehaviour 
{
	private Rigidbody2D rb2d;
    private readonly int FRAME_X = 12;

	// Use this for initialization
	void Start () 
	{
		//Get and store a reference to the Rigidbody2D attached to this GameObject.
		rb2d = GetComponent<Rigidbody2D>();

		//Start the object moving.
		float waveSpeed = GameControl.instance.scrollSpeed * 2f;
		rb2d.velocity = new Vector2 (waveSpeed, 0);
	}

	void Update()
	{
		// If the game is over, stop scrolling.
		if (GameControl.instance.gameOver)
		{
			rb2d.velocity = Vector2.zero;
		}
        if (transform.position.x < -FRAME_X)
        {
            DestroyGameObject();
        }
	}

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}

