using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour 
{
	public static GameControl instance;			//A reference to our game control script so we can access it statically.
	public Text scoreText;						//A reference to the UI text component that displays the player's score.
	public GameObject gameOvertext;				//A reference to the object that displays the text which appears when the player dies.
    public GameObject hpComponent;
    public GameObject Player;
    public GameObject magnetPrefab;
    public GameObject dizzyBirdsPrefab;
    //private float speedUpInterval = 20f;
    //private float speedUpRate = -1f;
    public float maxSpeed = -10f;
    public float globalTimer = 0;
    //private float pTime = 0;                    //Timer for auto-speed up

	public int score = 0;						//The player's score.
	public bool gameOver = false;				//Is the game over?
	public float scrollSpeed = -1.5f;
    public float scoreRate = 1f;

    public bool updateStars = false;
    private Transform lastStars;
    private float updateStarsRate = 2f;
    private float timeSinceCollision = 0f;

	public int hp = 3;
	public GameObject[] hpHeartObjArr;

    public bool hasMagnet = false;
    private bool existMagnet = false;
    public bool hasDizzyBirds = false;
    private bool existDizzyBird = false;
    private float timeSinceMag = 0f;
    public Transform fairyWithMag;

    public float Boundary_LEFT = -12;
    public float Boundary_RIGHT = 15;

    private float nightTimeElapsed = 0f;
    private float nightTime = 60f;

    public float orbitDistance = 1.0f;
    public float orbitDegreesPerSec = 180.0f;
    public Vector3 relativeDistance = new Vector3(0, 0.5f);

    // Fade Effect:
    public GameObject myFade;

	void Awake() {
		//If we don't currently have a game control...
		if (instance == null) {
			//...set this one to be it...
			instance = this;
			//...otherwise...
		} else if (instance != this) {
			//...destroy this one because it is a duplicate.
			Destroy (gameObject);
		}

		hpHeartObjArr = new GameObject[] { 
			hpComponent.transform.Find ("heart1").gameObject,
			hpComponent.transform.Find ("heart2").gameObject,
			hpComponent.transform.Find ("heart3").gameObject
		};

        magnetPrefab = (GameObject)Instantiate(magnetPrefab, Player.transform.position, Quaternion.identity); 
        dizzyBirdsPrefab = (GameObject)Instantiate(dizzyBirdsPrefab, Player.transform.position, Quaternion.identity); 
        magnetPrefab.SetActive(false);
        dizzyBirdsPrefab.SetActive(false);

        score = SceneControl.Instance.score;
        scoreText.text = "Score: " + score.ToString();
        ReduceHP(3 - SceneControl.Instance.HP);
	}

	public int getHP() {
		return hp;
	}

	void Update() {

        globalTimer += Time.deltaTime;

        //Auto load scene when time's up
        nightTimeElapsed += Time.deltaTime;
        if (nightTimeElapsed > nightTime)
        {
            nightTimeElapsed = 0f;
            SceneControl.Instance.HP = getHP();
            SceneControl.Instance.score = score;
            StartCoroutine(FadeScene());
        }

        //Auto speed up
//        if (globalTimer - pTime > speedUpInterval && scrollSpeed > maxSpeed)
//        {
//            pTime = globalTimer;
//            scrollSpeed += speedUpRate;
//        }
       

        if (updateStars) {
            timeSinceCollision += Time.deltaTime;
            if (timeSinceCollision >= updateStarsRate) {
                timeSinceCollision = 0f;
                updateStars = false;
                foreach (Transform child in lastStars) {
                    child.gameObject.SetActive(true);
                }
            }
        }
        if (hasDizzyBirds) 
        {
            if (!existDizzyBird)
            {
                dizzyBirdsPrefab.transform.position = Player.transform.position + new Vector3(0.45f, 0.68f);
                // Start Animation
                dizzyBirdsPrefab.SetActive(true);
                existDizzyBird = true;
            }
            else
            {
                dizzyBirdsPrefab.transform.position = Player.transform.position + new Vector3(0.45f, 0.68f);
            }
        } 
        else 
        {
            existDizzyBird = false;
            dizzyBirdsPrefab.SetActive(false);
        }
        if (hasMagnet) {
            timeSinceMag += Time.deltaTime;
            if (!existMagnet)
            {
                magnetPrefab.transform.position = Player.transform.position - new Vector3(1f,0);
                relativeDistance = magnetPrefab.transform.position - Player.transform.position;
                magnetPrefab.SetActive(true);
                existMagnet = true;
            }
            magnetPrefab.transform.position = Player.transform.position + relativeDistance;
            magnetPrefab.transform.RotateAround(Player.transform.position, new Vector3(0,1f,0), orbitDegreesPerSec * Time.deltaTime);
            // Reset relative position after rotate
            relativeDistance = magnetPrefab.transform.position - Player.transform.position;
            if (timeSinceMag >= 10f)
            {
                timeSinceMag = 0f;
                hasMagnet = false;
                existMagnet = false;
                fairyWithMag.GetChild(1).gameObject.SetActive(true);
                magnetPrefab.SetActive(false);
            }
        }
	}

    public void BirdScored(int s) {
		//The bird can't score if the game is over.
		if (gameOver) {
			return;
		}
		//If the game is not over, increase the score...
		score+=s;
		//...and adjust the score text.
		scoreText.text = "Score: " + score.ToString();
	}

	public void BirdDied() {
		//Activate the game over text.
		//gameOvertext.SetActive (true);
		//Set the game to be over.		
        nightTimeElapsed = 0f;
        SceneControl.Instance.score = score;
        gameOver = true;
	}

	public bool ReduceHP(int lostHPAmount) {
		// If lostHPAmount is more then hpLeft, consider death.
		if (hp - lostHPAmount < 0) {
			hp = lostHPAmount;
		}

		for (int i = hp - 1; i >= hp - lostHPAmount; i--) {
			hpHeartObjArr [i].SetActive (false);
		}

		hp -= lostHPAmount;

		// If no hp left, return true indicating death.
		if (hp == 0) {
			return true;
		} else {
			return false;
		}
    }
		
	public void IncreaseHP(int gainHPAmount) {
		// Can only increase HP to 3 at most.
		if (hp + gainHPAmount > 3) {
			gainHPAmount = 3 - hp;
		}

		for (int i = hp; i <= hp + gainHPAmount - 1; i++) {
			hpHeartObjArr [i].SetActive (true);
		}

		hp += gainHPAmount;
	}

    public void RenewStars(Transform parent) {
        updateStars = true;
        lastStars = parent;
    }

    IEnumerator FadeScene()
    {
        Time.timeScale = 0.5f;
        float time = myFade.GetComponent<FadeScene>().BeginFade(1);
        yield return new WaitForSeconds(time);
        Time.timeScale = 1f;
        SceneControl.Instance.LoadScene2();
    }
}
