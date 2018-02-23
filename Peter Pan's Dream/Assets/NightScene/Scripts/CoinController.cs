using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {

    private GameObject target;

    private float speed = 7f;
    private float initPositionX;
    private float initPositionY;

    // Use this for initialization
    void Start () {
        target = GameControl.instance.Player;
        initPositionX = transform.position.x;
        initPositionY = transform.position.y;
    }

    // Update is called once per frame
    void Update () {
        if (GameControl.instance.hasMagnet && transform.position.x <= 2 && transform.position.x >= -14)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }
    }

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.gameObject.name == "Bird")
//        {
//            transform.position = new Vector3(initPositionX, initPositionY);
//            transform.gameObject.SetActive(true);
//        }
//    }
}
