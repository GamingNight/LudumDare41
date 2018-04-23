using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;

    public static GameManager GetInstance()
    {

        return instance;
    }

    private void Awake()
    {

        if (instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    public void GameOver(EndGameStats.GameOverType type)
    {

        EndGameStats.GAME_OVER_TYPE = type;
        SceneManager.LoadScene("Gameover");
    }

    public void Win()
    {

        SceneManager.LoadScene("Win");
    }
}
