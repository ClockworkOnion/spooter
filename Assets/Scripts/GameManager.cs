using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour { 

    static GameManager instance = null;
    private uint score = 0;
    private Gamestate gamestate;
    private TextMeshProUGUI waveClearText;

    public bool IsGameOver
    {
        get => gamestate == Gamestate.gameOver;
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

     }

    public static GameManager GetManager()
    {
        return instance;
    }

    public void Start()
    {
        waveClearText = GameObject.Find("WaveClearText").GetComponent<TextMeshProUGUI>();
        gamestate = Gamestate.playing;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void GameOver()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        int destroyed = GameObject.Find("LevelManager").GetComponent<LevelManager>().totalShipsDestroyed;
        waveClearText.enabled = true;
        waveClearText.SetText($"Game Over!\nYour score was : {score}\nPress R to restart!\n Press Esc to Exit");
        gamestate = Gamestate.gameOver;
        ScoreBoard.GetScoreBoard().OnGameFinished(score);
        Debug.Log("Game over!");
    }

    public void AddScore(uint score){
        this.score += score;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded!");
        waveClearText = GameObject.Find("WaveClearText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gamestate == Gamestate.gameOver)
        {
            if (Input.GetKey(KeyCode.R) && ScoreBoard.GetScoreBoard().Restartable)
            {
                ScoreBoard.GetScoreBoard().OnGameRestart();
                score = 0;
                waveClearText.enabled = false;
                gamestate = Gamestate.playing;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (Input.GetKey(KeyCode.Escape)){
                SceneManager.LoadScene(1);
            }
        }
        
    }

    public enum Gamestate
    {
        playing,
        gameOver
    }
}
