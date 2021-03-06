﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMagnetism : MonoBehaviour {

    public Vector2 positionOffset = new Vector2(0.05f, -0.05f);
    public float amplitude=1;

    private GameObject player;
    private SpriteRenderer ballSprite;
    private SpriteRenderer playerSprite;
    private Rigidbody2D playerRgbd;

    private Vector2 speed;
    private float prevVerticalMove;
    private float prevHorizontalMove;
    private bool isMovingWithPlayer;


    void Start() {
        Vector2 playerPos = player.transform.position;
        transform.position = playerPos + positionOffset;
        playerSprite = player.GetComponent<SpriteRenderer>();
        playerRgbd = player.GetComponent<Rigidbody2D>();
        ballSprite = GetComponent<SpriteRenderer>();
        prevVerticalMove = 0;
        prevHorizontalMove = 0;
        isMovingWithPlayer = false;
    }

    public void UpdatePlayer(GameObject newPlayer)
    {
        player = newPlayer;
        playerSprite = player.GetComponent<SpriteRenderer>();
        playerRgbd = player.GetComponent<Rigidbody2D>();
    }

    void Update() {
        Vector2 playerPos = player.transform.position;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0) {
            speed = playerRgbd.velocity;
            float speedModule = speed.magnitude;
            float xOffset = positionOffset.x * horizontal + Mathf.Pow(speedModule,3) * amplitude * (horizontal * 0.05f * (Mathf.Abs(vertical) == 1 ? 0.5f : 1));
            float yOffset = positionOffset.y + Mathf.Pow(speedModule, 3) * amplitude* (vertical * 0.05f);
            transform.position = playerPos + new Vector2(xOffset, yOffset);
            prevVerticalMove = vertical;
            prevHorizontalMove = horizontal;
            isMovingWithPlayer = true;
        } else {
            Vector2 offset = positionOffset;
            if (prevHorizontalMove != 0)
                offset.x *= playerSprite.flipX ? -1 : 1;
            else if (prevVerticalMove > 0) {
                offset.x *= -1;
            }
            transform.position = playerPos + offset;
            isMovingWithPlayer = false;
        }
    }

    public bool IsMovingWithPlayer() {

        return isMovingWithPlayer;
    }
}
