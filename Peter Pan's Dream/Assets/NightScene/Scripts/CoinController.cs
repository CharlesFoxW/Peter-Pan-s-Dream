using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {

    private GameObject target;

    private float speed = 10f;

    // Use this for initialization
    void Start () {
        target = GameControl.instance.Player;
    }

    // Update is called once per frame
    void Update () {
        if (GameControl.instance.hasMagnet)
        {
            Vector2 distance = target.transform.position - transform.position;
            if (distance.magnitude < 5f) {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
            }
        }
    }
}
