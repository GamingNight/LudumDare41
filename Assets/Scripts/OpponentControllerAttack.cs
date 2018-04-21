using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentControllerAttack : MonoBehaviour {

    public GameObject player;
    public float speed;

    private bool attack;

    private void Start() {

        attack = true;
    }

    void Update() {

        if (attack) {
            //Vector2 lerpPosition = Vector2.Lerp(transform.position, player.transform.position, Time.deltaTime);
            //transform.LookAt(lerpPosition);
            Vector2 attackDirection = (transform.position - player.transform.position).normalized;
            Vector3 translation = new Vector3(attackDirection.x * speed * Time.deltaTime, attackDirection.y * speed * Time.deltaTime, 0f);
            transform.Translate(translation);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //Stop attacking when player is reached
        if (collision.gameObject == player) {
            attack = false;
        }
    }
}
