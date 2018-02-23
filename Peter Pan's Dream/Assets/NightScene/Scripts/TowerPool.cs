using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPool : MonoBehaviour {

    public GameObject towerPrefab;                                    //The column game object.
    public int towerPoolSize = 5;                                  //How many columns to keep on standby.
    public float spawnRate = 8f;                                    //How quickly columns spawn.

    private GameObject[] towers;                                   //Collection of pooled columns.
    private int currentTower = 0;                                  //Index of the current column in the collection.

    private Vector2 objectPoolPosition = new Vector2 (-15,-25);     //A holding position for our unused columns offscreen.
    private float spawnXPosition = 16f;

    private float timeSinceLastSpawned;


    void Start()
    {
        timeSinceLastSpawned = 0f;

        //Initialize the columns collection.
        towers = new GameObject[towerPoolSize];
        //Loop through the collection... 
        for(int i = 0; i < towerPoolSize; i++)
        {
            //...and create the individual columns.
            towers[i] = (GameObject)Instantiate(towerPrefab, objectPoolPosition, Quaternion.identity);
        }
    }


    //This spawns columns as long as the game is not over.
    void Update()
    {
        timeSinceLastSpawned += Time.deltaTime;

        if (GameControl.instance.gameOver == false && timeSinceLastSpawned >= spawnRate) 
        {   
            timeSinceLastSpawned = 0f;

            //...then set the current column to that position.
            towers[currentTower].transform.position = new Vector2(spawnXPosition, 0f);

            //Increase the value of currentColumn. If the new size is too big, set it back to zero
            currentTower ++;

            if (currentTower >= towerPoolSize) 
            {
                currentTower = 0;
            }
        }
    }
}
