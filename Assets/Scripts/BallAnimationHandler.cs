using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnimationHandler : MonoBehaviour {

    Animator animator;
    Rigidbody2D rgbd;
    BallMagnetism ballMagnetism;

    void Start() {
        animator = GetComponent<Animator>();
        rgbd = GetComponent<Rigidbody2D>();
        ballMagnetism = GetComponent<BallMagnetism>();
    }

    void Update() {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool movingWithPlayer = ballMagnetism.enabled && ballMagnetism.IsMovingWithPlayer();
        bool movingByItself = rgbd.velocity.sqrMagnitude != 0;
        animator.SetBool("roll", movingByItself || movingWithPlayer);
        if (movingWithPlayer) {
            animator.SetFloat("direction", horizontal != 0 ? horizontal : vertical);
        } else if (movingByItself) {
            animator.SetFloat("direction", rgbd.velocity.x);
        }

    }
}
