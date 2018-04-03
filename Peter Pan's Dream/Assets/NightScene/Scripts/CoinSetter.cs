using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSetter : MonoBehaviour {
    
    public GameObject goldCoinPrefab;
    public GameObject silverCoinPrefab;
    public GameObject bronzeConinPrefab;

    private int coinsAmount = 40;
    public int curGoldCoin = -1;
    public int curSilverCoin = -1;
    public int curBronzeCoin = -1;
    private int styleAmount = 5;
    private int selectedStyle;

    private CoinSetter coinSetter;

    List<GameObject> goldCoinList;
    List<GameObject> silverCoinList;
    List<GameObject> bronzeCoinList;

	// Use this for initialization
	void Start () {
        goldCoinList = new List<GameObject>();
        silverCoinList = new List<GameObject>();
        bronzeCoinList = new List<GameObject>();

        generateCoins(goldCoinPrefab, goldCoinList);
        generateCoins(silverCoinPrefab, silverCoinList);
        generateCoins(bronzeConinPrefab, bronzeCoinList);

        //For Test
        //coinSetter = FindObjectOfType<CoinSetter> ();
        //Vector2 startPosition = new Vector2(5,1f);
        //coinSetter.SetCoins(startPosition, 2);
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    public void SetCoins(Vector2 startPosition, int coinType){
        //if startPosition is lower than -2, then reset y to -2f
        if (startPosition.y < -2)
            startPosition = new Vector2(startPosition.x, -2f);
        else if (startPosition.y < 0)
            selectedStyle = Random.Range(1, 3);
        else
            selectedStyle = Random.Range(1, styleAmount + 1);
        switch (selectedStyle)
        {
            case 1: 
                SetLineThreeCoin(startPosition, coinType);
                break;
            case 2:
                SetSquareFourCoin(startPosition, coinType);
                break;
            case 3:
                SetSquareNineCoin(startPosition, coinType);
                break;
            case 4:
                SetVerticalLineFourCoin(startPosition, coinType);
                break;
            case 5:
                SetDiamondCoin(startPosition, coinType);
                break;
        } 
    }

    public void SetSingleCoin (Vector2 startPosition, int offsetX, int offsetY, int coinType){
        GameObject coin = getCoin(coinType);
        coin.SetActive(true);
        coin.transform.position = new Vector2(startPosition.x + (float)offsetX, startPosition.y + (float)offsetY);
    }

    public GameObject getCoin(int coinType){
        GameObject coin = null;
        switch (coinType)
        {
            case 1:
                {
                    curGoldCoin++;
                    if (curGoldCoin >= coinsAmount)
                    {
                        curGoldCoin = 0;
                    }
                    coin = goldCoinList[curGoldCoin];  
                }
                break;
            case 2:
                {
                    curSilverCoin++;
                    if (curSilverCoin >= coinsAmount)
                    {
                        curSilverCoin = 0;
                    }
                    coin = silverCoinList[curSilverCoin];  
                }
                break;
            case 3:
                {
                    curBronzeCoin++;
                    if (curBronzeCoin >= coinsAmount)
                    {
                        curBronzeCoin = 0;
                    }
                    coin = bronzeCoinList[curBronzeCoin];  
                }
                break;
        }
        return coin;
    }

    public void generateCoins(GameObject coinPrefab, List<GameObject> coinList){
        for (int i = 0; i < coinsAmount; i++)
        {
            GameObject coin = (GameObject)Instantiate(coinPrefab);
            coin.SetActive(false);
            coinList.Add(coin);
        }
    }

    public void SetLineThreeCoin(Vector2 startPosition, int coinType){
        for (int i = 0; i <= 2; i++)
        {
            SetSingleCoin(startPosition, i, 0, coinType);
        }
    }

    public void SetVerticalLineFourCoin(Vector2 startPosition, int coinType){
        for (int i = 0; i >= -3; i--)
        {
            SetSingleCoin(startPosition, 0, i, coinType);
        }
    }

    public void SetSquareFourCoin(Vector2 startPosition, int coinType){
        for (int i = 0; i <= 1; i++)
            for (int j = 0; j >= -1; j--)
            {
                SetSingleCoin(startPosition, i, j, coinType);
            }
    }

    public void SetSquareNineCoin(Vector2 startPosition, int coinType){
        for (int i = 0; i <= 2; i++)
            for (int j = 0; j >= -2; j--)
            {
                SetSingleCoin(startPosition, i, j, coinType);
            }
    }

    public void SetDiamondCoin(Vector2 startPosition, int coinType){
        for (int j = 0; j > -5; j--)
        {
            if (j == 0 || j == -4)
            {
                SetSingleCoin(startPosition, 0, j, coinType);
            }
            if (j == -1 || j == -3)
            {
                for (int i = -1; i <= 1; i++)
                    SetSingleCoin(startPosition, i, j, coinType);
            }
            if (j == -2)
            {
                for (int i = -2; i <= 2; i++)
                {
                    SetSingleCoin(startPosition, i, j, coinType);
                }
            }
        }
    }
}
