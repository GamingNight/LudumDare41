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

    private void OnTriggerEnter2D(Collider2D collision) {

        Debug.Log("ENTER");
        if(collision.gameObject.tag == "Player") {
            Debug.Log("PLAYER");
            playerIsDetected = true;
            spriteRenderer.color = new Color(255, 128, 128);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {

        Debug.Log("EXIT");
        if (collision.gameObject.tag == "Player") {
            Debug.Log("PLAYER");
            playerIsDetected = false;
            spriteRenderer.color = new Color(255, 255, 255);
        }
    }
}
