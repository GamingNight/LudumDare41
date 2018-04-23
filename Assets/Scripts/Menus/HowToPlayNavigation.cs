using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayNavigation : MonoBehaviour {

    public GameObject backText;
    public GameObject mainContainer;
    public GameObject howToPlayContainer;

    // Sounds
    private AudioSource[] sounds;
    private AudioSource bipSFX;
    private AudioSource validationSFX;
    private AudioSource backSFX;

    private void Start()
    {
        // Sounds
        sounds = transform.parent.GetComponents<AudioSource>();
        bipSFX = sounds[0];
        validationSFX = sounds[1];
        backSFX = sounds[2];
    }

    void Update() {
        bool submit = Input.GetButtonDown("Submit") || Input.GetMouseButtonDown(0);

        if (submit) {
            if (!backSFX.isPlaying)
            {
                backSFX.Play();
            }
            mainContainer.SetActive(true);
            howToPlayContainer.SetActive(false);
        }
    }
}
