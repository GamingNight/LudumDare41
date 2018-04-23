using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCalculation : MonoBehaviour {

    public float winPoints = 10;

    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        winPoints = 10;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Ball" && PlayerManager.GetInstance().GetScoringPoints() != 0) {
            if (PlayerManager.GetInstance().GetScoringPoints() > winPoints) {
                Debug.Log("YOU SCORED");
            } else {
                animator.SetBool("catchBall", true);
                Debug.Log("The goal keeper catched the ball!");
            }
        }
    }

    public void GameOverByCatch() {

        GameManager.GetInstance().GameOver();
    }
}
