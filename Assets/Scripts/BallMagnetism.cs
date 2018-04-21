﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMagnetism : MonoBehaviour {

    public GameObject player;

    private Vector2 pos;
    private float horizontalStore;
    private float verticalStore;
    private Vector2 speed;
    private Vector2 speedStore;


    // Use this for initialization
    void Start () {
        pos = player.transform.position;
        transform.position = pos + new Vector2(0.05F, -0.05F);
        Debug.Log(transform.position);
    }
	
	// Update is called once per frame
	void Update () {
        pos = player.transform.position;
        Rigidbody2D rgbd = player.GetComponent<Rigidbody2D>();
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            speed = rgbd.velocity;
            float speedModule = speed.magnitude;
            transform.position = pos + new Vector2((0.1F - 0.05F * Mathf.Abs(vertical)) * horizontal*speedModule, 0.05F * vertical*speedModule);
            speedStore = speed.normalized;
        }
        else
        {
            transform.position = pos + 0.05F*speedStore;
        }
    }
}
