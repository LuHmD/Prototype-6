using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    private GroundPiece[] allGroundPieces;

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI timeText;
    public Button restartButton;
    private float time;
    public bool isGameActive;

    public void Start()
    {
        isGameActive = true;
        time = 30.0f;
        SetupNewLevel();
    }

    private void SetupNewLevel()
    {
        allGroundPieces = FindObjectsOfType<GroundPiece>();
        

       

    }

  

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);

      
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        SetupNewLevel();
    }

    public void CheckComplete()
    {
        bool isFinished = true;

        for (int i = 0; i < allGroundPieces.Length; i++)
        {
            if (allGroundPieces[i].isColored == false)
            {
                isFinished = false;
                break;
            }
        }

        if (isFinished)
            
        NextLevel();
        
    }

    private void NextLevel()
    {
        FindObjectOfType<AudioManager>().Play("Success");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Start();

    }

    public void GameOver()
    {
        FindObjectOfType<AudioManager>().Play("GameOver");
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.onClick.AddListener(RestartGame);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Update()
    {
        if (isGameActive)
        {
            time -= Time.deltaTime;
            timeText.text = "Time:" + Mathf.Round(time);
            if (time < 0)
            {
                GameOver();
            }
        }
    }

}
