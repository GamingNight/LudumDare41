using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverDisplay : MonoBehaviour
{

    public GameObject timeOutText;
    public GameObject tacleText;
    public GameObject failShootText;

    private AudioSource validationAudio;

    void Start()
    {
        switch (EndGameStats.GAME_OVER_TYPE)
        {
            case EndGameStats.GameOverType.TIME_OUT:
                timeOutText.SetActive(true);
                break;
            case EndGameStats.GameOverType.CATCHED_BY_OPPONENT:
                tacleText.SetActive(true);
                break;
            case EndGameStats.GameOverType.GOOD_GOAL_KEEPER:
                failShootText.SetActive(true);
                break;
        }
        validationAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        bool submit = Input.GetButtonDown("Submit") || Input.GetMouseButtonDown(0);

        if (submit)
        {
            if (!validationAudio.isPlaying)
            {
                validationAudio.Play();
            }
            SceneManager.LoadScene("Menu");
        }
    }
}
