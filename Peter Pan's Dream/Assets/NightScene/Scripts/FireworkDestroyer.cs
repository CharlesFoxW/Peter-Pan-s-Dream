using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkDestroyer : MonoBehaviour {
    public float explodeTime = 1f;
    private float timeElapsed = 0f;
    private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        timeElapsed = 0f;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed, 0);
	}
	
	// Update is called once per frame
	void Update () {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= explodeTime)
        {
            DestroyGameObject();
        }
	}

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
