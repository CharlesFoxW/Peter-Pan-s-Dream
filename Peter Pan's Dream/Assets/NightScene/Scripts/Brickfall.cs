using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brickfall : MonoBehaviour {

    private Rigidbody2D rb2d;
    private readonly int FRAME_Y_BOTTOM = -6;
    public float fallspeed = -1.8f;
    public float rotatespeed = 135f;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        //rb2d.velocity = new Vector2 (GameControl.instance.scrollSpeed, fallspeed);
	}
	
	// Update is called once per frame
	void Update () {
        
        if (transform.position.y < FRAME_Y_BOTTOM)
        {
            DestroyGameObject();
        }
        if (!GameControl.instance.gameOver)
        {
            transform.Rotate (new Vector3 (0, 0, rotatespeed) * Time.deltaTime);
            transform.position = new Vector2(transform.position.x + GameControl.instance.scrollSpeed * Time.deltaTime,
                transform.position.y + fallspeed * Time.deltaTime);
        }
	}

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
