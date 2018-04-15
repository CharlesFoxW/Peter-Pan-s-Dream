using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneControl : MonoBehaviour {

    // Use this for initialization
    public static SceneControl Instance;
    public int HP;
    public int score;
    public int roundToNight;

	private ScoreManager theScoreManager;

    void Awake()
    {
		// If the instance reference has not been set, yet, 
        if (Instance == null)
        {
			// Set this instance as the instance reference.
            Instance = this;
        }
		else if (Instance != this)
        {
			// If the instance reference has already been set, and this is not the
			// the instance reference, destroy this game object.
            Destroy(gameObject);
        }

		// Do not destroy this object, when we load a new scene.
		DontDestroyOnLoad(gameObject);

    }
    public void Start () {

        SceneManager.LoadScene("MainScene");
        HP = 3;
		score = 0;
        roundToNight = 0;
		if (!PlayerPrefs.HasKey ("HighestScores")) {
			PlayerPrefs.SetInt ("HighestScores", 0);
		}

		theScoreManager = FindObjectOfType<ScoreManager>();
	}
		
	// Update is called once per frame
	void Update () {     
	}
    public void LoadScene1()
    {
        Debug.Log("ChangetoNight");
       
        SceneManager.LoadScene("Night");

        roundToNight++;
    }

    public void LoadScene2()
    {
        Debug.Log("ChangetoDay");

        SceneManager.LoadScene("Daytime");
    }
}
