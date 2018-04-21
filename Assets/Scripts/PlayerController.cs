using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float strength;
    public float runCoeff;

    private Rigidbody2D rgbd;
    private Vector3 movement;
    private float drag;
    private float i;

    // Use this for initialization
    void Start () {
        rgbd = GetComponent<Rigidbody2D>();
        drag = rgbd.drag;
        i = 0;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKey(KeyCode.V))
        {
            //Handle dash (interrupt walking)
            Dash();
        }
        //Handle regular walking
        Move();


    }

    private void Move()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            movement.Set(horizontal, vertical, 0f);
            rgbd.AddForce(movement.normalized * strength);
        }
        //    if (vertical < 0)
        //    {
        //        animator.SetInteger("walking", 1);
        //        lastWalkingAnimationState = 1;
        //    }
        //    else if (vertical > 0)
        //    {
        //        animator.SetInteger("walking", -1);
        //        lastWalkingAnimationState = -1;
        //    }
        //    else
        //    {
        //        animator.SetInteger("walking", lastWalkingAnimationState);
        //    }
        //    spriteRenderer.flipX = horizontal < 0;
        //    if (!walkAudioSource.isPlaying)
        //        walkAudioSource.Play();
        //}
        //else
        //{
        //    walkAudioSource.Stop();
        //    animator.SetInteger("walking", 0);
        //}
    }

    private void Dash ()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            movement.Set(horizontal, vertical, 0f);
            rgbd.AddForce(movement.normalized * strength * Mathf.Sqrt(runCoeff));
        }
        //    if (vertical < 0)
        //    {
        //        animator.SetInteger("walking", 1);
        //        lastWalkingAnimationState = 1;
        //    }
        //    else if (vertical > 0)
        //    {
        //        animator.SetInteger("walking", -1);
        //        lastWalkingAnimationState = -1;
        //    }
        //    else
        //    {
        //        animator.SetInteger("walking", lastWalkingAnimationState);
        //    }
        //    spriteRenderer.flipX = horizontal < 0;
        //    if (!walkAudioSource.isPlaying)
        //        walkAudioSource.Play();
        //}
        //else
        //{
        //    walkAudioSource.Stop();
        //    animator.SetInteger("walking", 0);
        //}
    }
}
