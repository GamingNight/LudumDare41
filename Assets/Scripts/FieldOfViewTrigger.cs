using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewTrigger : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private GameObject player;

    private void Start() {

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdatePlayer(GameObject newPlayer) {
        player = newPlayer;
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject == player) {
            PlayerManager.GetInstance().InstantiateAlly();
            spriteRenderer.color = new Color(154f / 255, 41f / 255, 28f / 255);
            //Deactivate patrol
            if (transform.parent.gameObject.GetComponent<OpponentControllerPatrol>() != null)
                transform.parent.gameObject.GetComponent<OpponentControllerPatrol>().enabled = false;
            else if (transform.parent.gameObject.GetComponent<OpponentControllerRound>() != null)
                transform.parent.gameObject.GetComponent<OpponentControllerRound>().enabled = false;
            else if (transform.parent.gameObject.GetComponent<OpponentControllerScanAngle>() != null)
                transform.parent.gameObject.GetComponent<OpponentControllerScanAngle>().enabled = false;
            else if (transform.parent.gameObject.GetComponent<OpponentControllerStatic>() != null)
                transform.parent.gameObject.GetComponent<OpponentControllerStatic>().enabled = false;
            //Activate attack
            transform.parent.gameObject.GetComponent<OpponentControllerAttack>().enabled = true;
        }
    }
}
