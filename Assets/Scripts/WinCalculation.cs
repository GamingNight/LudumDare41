using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCalculation : MonoBehaviour {

    public float winPoints = 10;

    private Animator animator;
    private bool stepBack;
    private float stepBackDuration;
    private float stepBackTimer;

    void Start() {
        animator = GetComponent<Animator>();
        winPoints = 10;
        stepBack = false;
        stepBackDuration = 1f;
        stepBackTimer = 0f;
    }

    private void Update() {

        if (stepBack) {
            stepBackTimer += Time.deltaTime;
            float newXPos = Mathf.Lerp(transform.position.x, transform.position.x + 0.49f, stepBackTimer / stepBackDuration);
            transform.position = new Vector2(newXPos, transform.position.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Ball" && PlayerManager.GetInstance().GetScoringPoints() != 0) {
            if (PlayerManager.GetInstance().GetScoringPoints() > winPoints) {
                Debug.Log("YOU SCORED");
                other.gameObject.SetActive(false);
                animator.SetBool("looseBall", true);
            } else {
                other.gameObject.SetActive(false);
                animator.SetBool("catchBall", true);
                Debug.Log("The goal keeper catched the ball!");
            }
        }
    }

    public void GoalKeeperStepBack() {

        stepBack = true;
    }

    public void GameOverByCatch() {

        GameManager.GetInstance().GameOver();
    }
}
