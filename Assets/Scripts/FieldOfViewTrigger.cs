using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewTrigger : MonoBehaviour {

    SpriteRenderer spriteRenderer;
    bool playerIsDetected;

    private void Start() {

        spriteRenderer = GetComponent<SpriteRenderer>();
        playerIsDetected = false;
    }

    private void Update() {

        Debug.Log(spriteRenderer.color);
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player") {
            playerIsDetected = true;
            spriteRenderer.color = new Color(1, 128f / 255, 128f / 255);
        }
    }
}
