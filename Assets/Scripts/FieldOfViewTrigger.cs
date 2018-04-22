using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewTrigger : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private GameObject player;

    private void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdatePlayer(GameObject newPlayer)
    {
        player = newPlayer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject == player)
        {
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
