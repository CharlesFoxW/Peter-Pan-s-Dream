using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour {

	private readonly float maxHeight = 2f;

	public ObjectPooler coinPool;
	public float distanceBetweenCoins;


	private float curHeightIncrease;


	public void SpawnCoins (Vector3 startPosition, float width){

		int selector = 8;
		int pickStyle = Random.Range (0, selector + 1);

		curHeightIncrease = Random.Range (0 , maxHeight - startPosition.y);

		switch (pickStyle){
		case 0 :
			printRectangle (startPosition, Random.Range (1, 4), Random.Range (1, Mathf.FloorToInt (width)));
			break;
		case 1 :
			printHollowRectangle (startPosition,Random.Range (1, 4), Random.Range (1, Mathf.FloorToInt (width)));
			break;
		case 2:	
			printHollowHeart (startPosition, Random.Range (1, Mathf.FloorToInt (width / 3)));
			break;
		case 3 :
			printHeart (startPosition, Random.Range (1, Mathf.FloorToInt (width/3)));
			break;
		case 4:	
			printLeftSemiPyramid (startPosition, Mathf.FloorToInt (width / 3));
			break;
		case 5:
			printRightSemiPyramid (startPosition, Mathf.FloorToInt (width / 3));
			break;
		case 6:
			printDiamand (startPosition,Mathf.FloorToInt (width / 3));
			break;
		case 8:
			printHollowDiamand (startPosition, Mathf.FloorToInt (width / 3));
			break;
		default:
			break;
		}

	}





	private void printRectangle (Vector3 startPosition, int height, int length){
		int x, y;
		for(x = 0; x < length; x++){
			for(y = 0; y < height; y++){
				setCoin (startPosition, x - length/2, y, 0);
			}
		}
	}

	private void printHollowRectangle (Vector3 startPosition, int height, int length){
		int x, y;
		for(x = 0; x < length; x++){
			for(y = 0; y < height; y++){
				if (x == 0 || y == 0 || x == length - 1 || y == height - 1){
					setCoin (startPosition, x - length/2, y, 0);
				}
			}
		}
	}

	private void printHollowHeart (Vector3 startPosition, int rows){
		int x = 0;
		int y = 0;

		for(y = 1; y <= rows - 1; y ++){  
			for ( x = -rows; x <= 0; x ++){
				if (y + x == 0 || x == -rows){
					setCoin (startPosition, x, y, rows);
				}
			}

			for ( x = 0; x <= rows; x ++){
				if (y - x == 0 || x == rows){
					setCoin (startPosition, x , y, rows);
				}

			}
		} 

		for(y = 0; y >= -rows; y --){  
			for ( x = -rows; x < 0; x ++){
				if (x + y == -rows){
					setCoin (startPosition, x, y, rows);
				}
			}

			for ( x = 0; x <= rows; x ++){
				if (x - y == rows){
					setCoin (startPosition, x, y, rows);
				}

			}
		}  

	}

	private void printHeart (Vector3 startPosition, int rows){
		int x = 0;
		int y = 0;

		for(y = 1; y <= rows - 1; y ++){  
			for ( x = -rows; x <= 0; x ++){
				if (y+x <= 0){
					setCoin (startPosition, x, y, rows);
				}
			}

			for ( x = 0; x <= rows; x ++){
				if (y - x <= 0 ){
					setCoin (startPosition, x , y, rows);
				}

			}
		} 

		for(y = 0; y >= -rows; y --){  
			for ( x = -rows; x < 0; x ++){
				if (x + y >= -rows){
					setCoin (startPosition, x, y, rows);
				}
			}

			for ( x = 0; x <= rows; x ++){
				if (x - y <= rows){
					setCoin (startPosition, x, y, rows);
				}

			}
		}  

	}


	private void printLeftSemiPyramid (Vector3 startPosition, int rows){
		int x = 0;
		int y = 0;

		for(y = 0; y >= -rows; y --){  
			for ( x = -rows; x <= 0; x ++){
				if (x + y >= -rows){
					setCoin (startPosition, x, y, rows);
				}
			}

		}  

	}


	private void printRightSemiPyramid (Vector3 startPosition, int rows){
		int x = 0;
		int y = 0;

		for(y = 0; y >= -rows; y --){  
			for ( x = 0; x <= rows; x ++){
				if (x - y <= rows){
					setCoin (startPosition, x, y, rows);
				}

			}
		}  

	}


	private void printDiamand(Vector3 startPosition, int rows){
		int x = 0;
		int y = 0;

		for(y = 0; y <= rows; y ++){  
			for ( x = -rows; x <= 0; x ++){
				if (x - y >= -rows){
					setCoin (startPosition, x, y, rows);
				}
			}

			for ( x = 0; x < rows; x ++){
				if (x + y <= rows){
					setCoin (startPosition, x, y, rows);
				}

			}
		} 

		for(y = 0; y >= -rows; y --){  
			for ( x = -rows; x <= 0; x ++){
				if (x + y >= -rows){
					setCoin (startPosition, x, y, rows);
				}
			}

			for ( x = 0; x <= rows; x ++){
				if (x - y <= rows){
					setCoin (startPosition, x, y, rows);
				}

			}
		}  
	}


	private void printHollowDiamand(Vector3 startPosition, int rows){
		int x = 0;
		int y = 0;

		for(y = 0; y <= rows; y ++){  
			for ( x = -rows; x <= 0; x ++){
				if (x - y == -rows){
					setCoin (startPosition, x, y, rows);
				}
			}

			for ( x = 0; x < rows; x ++){
				if (x + y == rows){
					setCoin (startPosition, x, y, rows);
				}

			}
		} 

		for(y = 0; y >= -rows; y --){  
			for ( x = -rows; x <= 0; x ++){
				if (x + y == -rows){
					setCoin (startPosition, x, y, rows);
				}
			}

			for ( x = 0; x <= rows; x ++){
				if (x - y == rows){
					setCoin (startPosition, x, y, rows);
				}

			}
		}  
	}


	private void setCoin(Vector3 startPosition, int x, int y, int minHeightOffset){
		GameObject coin = coinPool.GetPooledObject ();
		coin.transform.position = new Vector3 (startPosition.x  + x * distanceBetweenCoins, startPosition.y + curHeightIncrease + minHeightOffset + y*distanceBetweenCoins, startPosition.z);
		coin.SetActive (true);
	}

}

