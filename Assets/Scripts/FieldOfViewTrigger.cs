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

        if (collision.gameObject.tag == "Player") {
            playerIsDetected = true;
            spriteRenderer.color = new Color(1, 128f / 255, 128f / 255);
            //Deactivate patrol, activate attack
            if (transform.parent.gameObject.GetComponent<OpponentControllerPatrol>() != null)
                transform.parent.gameObject.GetComponent<OpponentControllerPatrol>().enabled = false;
            else if (transform.parent.gameObject.GetComponent<OpponentControllerRound>() != null)
                transform.parent.gameObject.GetComponent<OpponentControllerRound>().enabled = false;
            else if (transform.parent.gameObject.GetComponent<OpponentControllerScanAngle>() != null)
                transform.parent.gameObject.GetComponent<OpponentControllerScanAngle>().enabled = false;

            transform.parent.gameObject.GetComponent<OpponentControllerAttack>().enabled = true;
        }
    }
}
