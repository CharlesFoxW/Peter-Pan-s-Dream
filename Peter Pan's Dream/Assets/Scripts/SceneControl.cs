using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneControl : MonoBehaviour {

    // Use this for initialization
    public static SceneControl Instance;
    public int HP;
    public int score;
    public Scene now;

	private ScoreManager theScoreManager;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }
    public void Start () {

        SceneManager.LoadScene("MainScene");
        HP = 3;score = 0;
		theScoreManager = FindObjectOfType<ScoreManager>();
	}
	public void daybegin()
    {
        SceneManager.LoadScene("Daytime"); HP = 3; score = 0;
    }
	// Update is called once per frame
	void Update () {     
	}
    public void LoadScene1()
    {
        Debug.Log("ChangetoNight");
        SceneControl.Instance.HP = 3;
		SceneControl.Instance.score = (int)theScoreManager.scoreCount;
        SceneManager.LoadScene("Night");
    }
    public void LoadScene2()
    {
        Debug.Log("ChangetoDay");
        SceneControl.Instance.HP = 3;
        SceneControl.Instance.score = 0;
        SceneManager.LoadScene("Daytime");
    }
}
