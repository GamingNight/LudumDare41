using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMagnetism : MonoBehaviour {

    public GameObject player;
    public Vector2 positionOffset = new Vector2(0.05f, -0.05f);

    private SpriteRenderer ballSprite;
    private SpriteRenderer playerSprite;
    private Rigidbody2D playerRgbd;

    private Vector2 speed;
    private int playerSortingOrder;

    private float prevVerticalMove;
    private float prevHorizontalMove;


    void Start() {
        Vector2 playerPos = player.transform.position;
        transform.position = playerPos + positionOffset;
        ballSprite = GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();
        playerRgbd = player.GetComponent<Rigidbody2D>();
        playerSortingOrder = playerSprite.sortingOrder;
        prevVerticalMove = 0;
        prevHorizontalMove = 0;
    }

    void Update() {
        Vector2 playerPos = player.transform.position;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0) {
            speed = playerRgbd.velocity;
            float speedModule = speed.magnitude;
            float xOffset = positionOffset.x * horizontal + 0.05f * (horizontal * 0.05f * (Mathf.Abs(vertical) == 1 ? 0.5f : 1));
            float yOffset = positionOffset.y + 0.05f * (vertical * 0.05f);
            transform.position = playerPos + new Vector2(xOffset, yOffset);
            prevVerticalMove = vertical;
            prevHorizontalMove = horizontal;
        } else {
            Vector2 offset = positionOffset;
            if (prevHorizontalMove != 0)
                offset.x *= playerSprite.flipX ? -1 : 1;
            else if (prevVerticalMove > 0) {
                offset.x *= -1;
            }
            transform.position = playerPos + offset;
        }
    }
}
