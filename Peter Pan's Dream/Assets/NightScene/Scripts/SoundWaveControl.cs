using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaveControl : MonoBehaviour {

	public GameObject wave;
	private Rigidbody2D rb2d;
	private float timeElaspe = 0f;
    private readonly int MAX_WAVE = 2;
    private int curWave = 0;

	public float shootInterval = 1f;

	void Start () {
		rb2d = GetComponent<Rigidbody2D>(); // BatWithWave's Rigidbody2D.
		//rb2d.velocity = new Vector2 (GameControl.instance.scrollSpeed, 0); // Make speed of BatWithWave to be the same as background.
	}

	void Update () {
        if (!GameControl.instance.gameOver && transform.position.x < GameControl.instance.Boundary_RIGHT 
            && transform.position.x > GameControl.instance.Boundary_LEFT) {
			// When in the frame scene.
            transform.position = new Vector2(transform.position.x + GameControl.instance.scrollSpeed * Time.deltaTime, 
                transform.position.y);
			timeElaspe += Time.deltaTime;
            if (timeElaspe >= shootInterval && curWave < MAX_WAVE) {
                Instantiate(wave, new Vector2(transform.position.x-2.2f,transform.position.y-0.3f), Quaternion.identity);
                curWave++;
				timeElaspe = 0f;
			}
        } else if (transform.position.x < GameControl.instance.Boundary_LEFT) {
			// When move out of the scene.
            timeElaspe = 0;
            curWave = 0;
		} 
	}
}
