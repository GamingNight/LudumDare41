using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentControllerStatic : MonoBehaviour {

    public GameObject fieldOfViewTrigger;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start() {

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (fieldOfViewTrigger.transform.eulerAngles.z >= 45 && fieldOfViewTrigger.transform.eulerAngles.z <= 90) {
            animator.SetInteger("direction", -1);
            spriteRenderer.flipX = false;
        } else if (fieldOfViewTrigger.transform.eulerAngles.z > 90 && fieldOfViewTrigger.transform.eulerAngles.z <= 180) {
            animator.SetInteger("direction", -1);
            spriteRenderer.flipX = true;
        } else if (fieldOfViewTrigger.transform.eulerAngles.z >= 225 && fieldOfViewTrigger.transform.eulerAngles.z <= 315) {
            animator.SetInteger("direction", 1);
        } else if (fieldOfViewTrigger.transform.eulerAngles.z > 135 && fieldOfViewTrigger.transform.eulerAngles.z < 225) {
            animator.SetInteger("direction", 0);
            spriteRenderer.flipX = true;
        } else {
            animator.SetInteger("direction", 0);
            spriteRenderer.flipX = false;
        }
    }
}
