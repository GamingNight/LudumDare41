﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float strength;
    public float boost;
    public float stamina;
    public float coco;

    private Rigidbody2D rgbd;
    private Vector2 movement;
    private float drag;
    private float i;
    private float iStamina;
    private float indivBoost;

    // Use this for initialization
    void Start () {
        rgbd = GetComponent<Rigidbody2D>();
        iStamina = stamina;
        strength = strength / Mathf.Pow(coco, 1);
        rgbd.mass = rgbd.mass / Mathf.Pow(coco, -1);
        indivBoost = stamina / 10;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKey(KeyCode.V) && iStamina != 2000 && indivBoost >0)
        {
            //Handle dash (interrupt walking)
            Dash();
            indivBoost = indivBoost - 1;
            if (indivBoost == stamina/10 - 1)
            {
                iStamina = iStamina - 1;
            }
            //Debug.Log(iStamina);
        }
        if (Input.GetKey(KeyCode.V)==false)
        {
            indivBoost = stamina / 10;
        }
        //Handle regular walking
        Move();
        Debug.Log(rgbd.velocity.magnitude);
        
    }

    private void Move()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            movement.Set(horizontal, vertical);
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
            movement.Set(horizontal, vertical);
//            rgbd.velocity = rgbd.velocity + movement.normalized * boost;
            rgbd.AddForce(movement.normalized * strength * boost);
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
