using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySpeed : MonoBehaviour
{

    public enum AnimationState
    {
        IDLE = 0, SIDE_WALK = 1, BACK_WALK = -1, FRONT_WALK = 2
    }

    public float speedUp;

    private Rigidbody2D rgbd;
    private Animator animator;
    private AnimationState animState;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animState = AnimationState.SIDE_WALK;
    }

    // Update is called once per frame
    void Update()
    {
        rgbd.velocity = rgbd.velocity + speedUp * new Vector2(1f, 0f) * Time.deltaTime;
        UpdateAnimation();

    }

    private void UpdateAnimation()
    {
        if (rgbd.velocity.sqrMagnitude != 0)
        {
            if (rgbd.velocity.x == 0)
            {
                if (rgbd.velocity.y > 0)
                {
                    animState = AnimationState.BACK_WALK;
                }
                else
                {
                    animState = AnimationState.FRONT_WALK;
                }
            }
            else
            {
                animState = AnimationState.SIDE_WALK;
            }
            spriteRenderer.flipX = rgbd.velocity.x < 0;
        }
        else
        {
            animState = AnimationState.IDLE;
        }
        animator.SetInteger("walkState", (int)animState);
    }
}
