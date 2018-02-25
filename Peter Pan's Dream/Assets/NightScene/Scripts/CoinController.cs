using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {

    private GameObject target;

    private float speed = 7f;

    // Use this for initialization
    void Start () {
        target = GameControl.instance.Player;
    }

    // Update is called once per frame
    void Update () {
        if (GameControl.instance.hasMagnet && transform.position.x <= 2 && transform.position.x >= -10)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }
    }
}
