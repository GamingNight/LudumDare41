using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private static GameManager instance;

    public static GameManager GetInstance() {

        return instance;
    }

    private void Awake() {

        if (instance == null) {
            instance = this;
        }
    }

    public void GameOver() {

        Debug.Log("GameOver");
    }

    public void Win() {
        Debug.Log("Win");
    }
}
