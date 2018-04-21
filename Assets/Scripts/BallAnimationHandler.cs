using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnimationHandler : MonoBehaviour {

    Animator animator;
    Rigidbody2D rgbd;

    void Start() {
        animator = GetComponent<Animator>();
        rgbd = GetComponent<Rigidbody2D>();
    }

    void Update() {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        animator.SetBool("roll", rgbd.velocity.sqrMagnitude != 0 || horizontal != 0 || vertical != 0);
        animator.SetFloat("direction", horizontal != 0 ? horizontal : vertical);
    }
}
