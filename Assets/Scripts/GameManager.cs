using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour { 

    static GameManager instance = null;
    private Gamestate gamestate;
    private TextMeshProUGUI waveClearText;


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
    }

    public void GameOver()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        waveClearText.enabled = true;
        waveClearText.SetText("Game Over!\nPress R to restart!");
        gamestate = Gamestate.gameOver;
        Debug.Log("Game over!");
    }

    // Update is called once per frame
    void Update()
    {
        if (gamestate == Gamestate.gameOver)
        {
            if (Input.GetKey("r"))
            {
                waveClearText.enabled = false;
                gamestate = Gamestate.playing;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        
    }

    public enum Gamestate
    {
        playing,
        gameOver
    }
}
