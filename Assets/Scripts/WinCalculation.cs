using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCalculation : MonoBehaviour {

    public float winPoints;

    private Animator animator;
<<<<<<< HEAD
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
=======
    private Rigidbody2D rgbd;

    void Start() {
        animator = GetComponent<Animator>();
        rgbd = GetComponent<Rigidbody2D>();
>>>>>>> 901ab146a61b23a6f8ee034c29c65550b50f7e8a
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Ball" && PlayerManager.GetInstance().GetScoringPoints() != 0) {
            if (PlayerManager.GetInstance().GetScoringPoints() > winPoints) {
<<<<<<< HEAD
                Debug.Log("YOU SCORED");
                other.gameObject.SetActive(false);
                animator.SetBool("looseBall", true);
=======
                Debug.Log("YOU SCORED"+ PlayerManager.GetInstance().GetScoringPoints());
                //Recul();
>>>>>>> 901ab146a61b23a6f8ee034c29c65550b50f7e8a
            } else {
                animator.SetBool("catchBall", true);
                Debug.Log("The goal keeper catched the ball!" + PlayerManager.GetInstance().GetScoringPoints());
            }
        }
    }

    public void GoalKeeperStepBack() {

        stepBack = true;
    }

    public void GameOverByCatch() {

        GameManager.GetInstance().GameOver();
    }
<<<<<<< HEAD

    public void PlayerWins() {
        GameManager.GetInstance().Win();
    }
=======
    //void Recul()
    //{
    //    rgbd.velocity = new Vector2(10f, 0);
    //    Debug.Log("pommier"+rgbd.velocity);
    //}
>>>>>>> 901ab146a61b23a6f8ee034c29c65550b50f7e8a
}
