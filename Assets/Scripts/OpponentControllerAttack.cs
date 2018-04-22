using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentControllerAttack : MonoBehaviour
{

    public float speed;

    private GameObject player;
    private bool attack;
    private float attackLerpRatio;

    private void Start()
    {
        attack = true;
        attackLerpRatio = 0f;
    }

    public void UpdatePlayer(GameObject newPlayer)
    {
        player = newPlayer;
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "OpponentFieldOfView")
            {
                child.GetComponent<FieldOfViewTrigger>().UpdatePlayer(newPlayer);
            }
        }
    }

    void Update()
    {

        if (attack)
        {

            //2D Look at
            attackLerpRatio += Time.deltaTime / 2f;
            if (attackLerpRatio < 1)
            {
                Vector3 difference = player.transform.position - transform.position;
                float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                Quaternion lerpQ = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0.0f, 0.0f, rotationZ), attackLerpRatio);
                transform.rotation = lerpQ;
            }
            else
            {
                Vector3 difference = player.transform.position - transform.position;
                float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
            }

            //Translation to player's position
            Vector2 attackDirection = (player.transform.position - transform.position).normalized;
            Vector3 translation = new Vector3(attackDirection.x * speed * Time.deltaTime, attackDirection.y * speed * Time.deltaTime, 0f);
            transform.Translate(translation, Space.World);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Stop attacking when player is reached
        if (collision.gameObject == player)
        {
            attack = false;
        }
    }
}
