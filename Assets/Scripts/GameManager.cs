using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour { 

    static GameManager instance = null;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
     }

    public static GameManager GetManager()
    {
        return instance;
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
