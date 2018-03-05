using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackgroundMountain : MonoBehaviour {

    private float boundary = 37f;
    private float scrollSpeed;

    // Use this for initialization
    void Start () {
        scrollSpeed = GameControl.instance.scrollSpeed / 2;
    }

    // Update is called once per frame
    void Update () {
        if (!GameControl.instance.gameOver)
        {
            transform.position = new Vector2(transform.position.x + scrollSpeed * Time.deltaTime, transform.position.y);
            if (transform.position.x <= -boundary)
                transform.position = new Vector2(boundary, transform.position.y);
        }
    }
}
