using UnityEngine;

public class SingleCoinControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x >= 13f) 
        {
            transform.gameObject.SetActive(true);
        }
        if (transform.position.x <= -12f)
        {
            transform.gameObject.SetActive(false);
            transform.position = new Vector3(-20f, 0f);
        }
	}
}
