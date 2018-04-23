using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinDisplay : MonoBehaviour
{
    private AudioSource validationAudio;
    private bool triggerMenuScene;

    void Start()
    {
        validationAudio = GetComponent<AudioSource>();
        triggerMenuScene = false;
    }

    private void Update()
    {
        bool submit = Input.GetButtonDown("Submit") || Input.GetMouseButtonDown(0);

        if (submit)
        {
            if (!validationAudio.isPlaying)
            {
                validationAudio.Play();
                triggerMenuScene = true;
            }
        }
        if (triggerMenuScene && !validationAudio.isPlaying)
            SceneManager.LoadScene("Menu");
    }
}
