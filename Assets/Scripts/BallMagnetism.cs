using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMagnetism : MonoBehaviour {

    public GameObject player;
    public Vector2 positionOffset = new Vector2(0.05f, -0.05f);

    private float horizontalStore;
    private float verticalStore;
    private Vector2 speed;
    private SpriteRenderer ballSprite;
    private SpriteRenderer playerSprite;
    private int playerSortingOrder;
    private float prevVerticalMove;


    // Use this for initialization
    void Start() {
        Vector2 playerPos = player.transform.position;
        transform.position = playerPos + positionOffset;
        ballSprite = GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();
        playerSortingOrder = playerSprite.sortingOrder;
        prevVerticalMove = 0;
    }

    // Update is called once per frame
    void Update() {
        Vector2 playerPos = player.transform.position;
        Rigidbody2D rgbd = player.GetComponent<Rigidbody2D>();
        SpriteRenderer spriterRenderer = player.GetComponent<SpriteRenderer>();
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0) {
            speed = rgbd.velocity;
            float speedModule = speed.magnitude;
            float xOffset = positionOffset.x * horizontal + speedModule * (horizontal * 0.05f * (Mathf.Abs(vertical) == 1 ? 0.5f : 1));
            float yOffset = positionOffset.y + speedModule * (vertical * 0.05f);
            transform.position = playerPos + new Vector2(xOffset, yOffset);
            ballSprite.sortingOrder = vertical > 0 ? playerSortingOrder + 1 : playerSortingOrder - 1;
            prevVerticalMove = vertical;
        } else {
            Vector2 offset = new Vector2(spriterRenderer.flipX ? -positionOffset.x : positionOffset.x, positionOffset.y);
            if (prevVerticalMove > 0) {
                offset.x *= -1;
            }
            transform.position = playerPos + offset;
        }
    }
}
