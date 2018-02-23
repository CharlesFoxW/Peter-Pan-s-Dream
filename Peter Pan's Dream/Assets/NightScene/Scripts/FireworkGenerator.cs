using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkGenerator : MonoBehaviour {
    private float fireCountDown = 0.5f;
    private float timeElapsed = 0f;
    public float firespeed = 2f;
    private float MAX_Y = 3f;
    private float MIN_Y = 0f;
    private float randomY = 0;
    private bool fired = false;
    private Rigidbody2D rb2d;
    private Animator anim;
    public GameObject exploded;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetTrigger("Fire");
        randomY = Random.Range(MIN_Y, MAX_Y);
        //rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameControl.instance.gameOver)
        {
            if (!fired)
            {
                timeElapsed += Time.deltaTime;
                transform.position = new Vector2(transform.position.x + GameControl.instance.scrollSpeed * Time.deltaTime,
                    transform.position.y);
                if (timeElapsed >= fireCountDown)
                {
                    fired = true;
                }
            }
            else
            {
                if (transform.position.y < randomY)
                {
                    //rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed, firespeed);
                    transform.position = new Vector2(transform.position.x + GameControl.instance.scrollSpeed * Time.deltaTime,
                        transform.position.y + firespeed * Time.deltaTime);
                }
                else
                {
                    Instantiate(exploded, new Vector2(transform.position.x,transform.position.y), Quaternion.identity);
                    DestroyGameObject();
                }
            }
        }

	}

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
