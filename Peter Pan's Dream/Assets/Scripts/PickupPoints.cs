using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPoints : MonoBehaviour {

	public int scoreToGive;

	private AudioSource[] aslist;
	private AudioSource asCoin;

	private ScoreManager theScoreManager;

	// Use this for initialization
	void Start () {
		aslist = FindObjectsOfType<AudioSource> ();
		theScoreManager = FindObjectOfType<ScoreManager> ();
		for (int i = 0; i < aslist.Length; i++) {
			if (aslist [i].name == "Coin") {
				asCoin = aslist [i];
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Player"){
			asCoin.Play ();
			theScoreManager.AddScore (scoreToGive);
			gameObject.SetActive (false);
		}
	}
}
