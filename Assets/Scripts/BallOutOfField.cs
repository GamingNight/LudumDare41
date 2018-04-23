using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallOutOfField : MonoBehaviour
{

    public GameObject outText;
    private AudioSource whistleAudio;

    private bool triggerTimer;
    private float timer;

    private void Start()
    {
        whistleAudio = GetComponent<AudioSource>();
        triggerTimer = false;
        timer = 0f;
    }

    private void Update()
    {
        if (triggerTimer)
        {
            timer += Time.deltaTime;
            if (timer >= 1 && !whistleAudio.isPlaying)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Ball")
        {
            outText.SetActive(true);
            triggerTimer = true;
            whistleAudio.Play();
        }
    }
}
