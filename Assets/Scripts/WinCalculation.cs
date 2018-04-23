using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCalculation : MonoBehaviour {

    public float winPoints;

    private Animator animator;
    private Rigidbody2D rgbd;

    void Start() {
        animator = GetComponent<Animator>();
        rgbd = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Ball" && PlayerManager.GetInstance().GetScoringPoints() != 0) {
            if (PlayerManager.GetInstance().GetScoringPoints() > winPoints) {
                Debug.Log("YOU SCORED"+ PlayerManager.GetInstance().GetScoringPoints());
                //Recul();
            } else {
                animator.SetBool("catchBall", true);
                Debug.Log("The goal keeper catched the ball!" + PlayerManager.GetInstance().GetScoringPoints());
            }
        }
    }

    public void GameOverByCatch() {

        GameManager.GetInstance().GameOver();
    }
    //void Recul()
    //{
    //    rgbd.velocity = new Vector2(10f, 0);
    //    Debug.Log("pommier"+rgbd.velocity);
    //}
}
