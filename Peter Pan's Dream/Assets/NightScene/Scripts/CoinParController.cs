using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinParController : MonoBehaviour {
    
//    public GameObject coin;

    private Vector3 [] coinPosition;
    private Vector3 coinParPosition;

	// Use this for initialization
	void Start () {
        coinPosition = new Vector3[transform.childCount];
        int i = 0;
        foreach (Transform child in transform)
        {
            coinPosition[i] = child.position;
            i++;
        }
        coinParPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x <= -13)
        {
            transform.position = coinParPosition;
            int i = 0;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
                child.position = coinPosition[i];
                i++;
            }
        }
	}
}
