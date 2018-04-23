using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager instance;

    public static GameManager GetInstance() {

        return instance;
    }

    public enum GameOverType {

        GOOD_GOAL_KEEPER, CATCHED_BY_OPPONENT, TIME_OUT
    }

    private GameOverType gameOverType;

    private void Awake() {

        if (instance == null) {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    public void GameOver(GameOverType type) {

        gameOverType = type;
        SceneManager.LoadScene("Gameover");
    }

    public void Win() {

        SceneManager.LoadScene("Win");
    }
}
