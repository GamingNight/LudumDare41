using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentControllerAttack : MonoBehaviour
{

    public enum AnimationState
    {

        WALK_FRONT_SIDE = 0, WALK_FRONT = 1, WALK_BACK = -1
    }

    public float speed;
    public GameObject fieldOfViewTrigger;

    private GameObject player;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool attack;
    private float attackLerpRatio;
    private AnimationState animState;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        attack = true;
        attackLerpRatio = 0f;
        animState = AnimationState.WALK_FRONT;
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
            //Field of view 2D Look at
            attackLerpRatio += Time.deltaTime / 2f;
            if (attackLerpRatio < 1)
            {
                Vector3 difference = player.transform.position - transform.position;
                float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                Quaternion lerpQ = Quaternion.Lerp(fieldOfViewTrigger.transform.rotation, Quaternion.Euler(0.0f, 0.0f, rotationZ), attackLerpRatio);
                fieldOfViewTrigger.transform.rotation = lerpQ;
            }
            else
            {
                Vector3 difference = player.transform.position - transform.position;
                float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                fieldOfViewTrigger.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
            }

            //Translation to player's position
            Vector2 attackDirection = (player.transform.position - transform.position).normalized;
            Vector3 translation = new Vector3(attackDirection.x * speed * Time.deltaTime, attackDirection.y * speed * Time.deltaTime, 0f);
            transform.Translate(translation, Space.World);

            UpdateAnimation(attackDirection);
        }
    }

    private void UpdateAnimation(Vector2 attackDirection)
    {

        animator.SetBool("walk", true);

        float x = attackDirection.x;
        float y = attackDirection.y;
        if (Mathf.Abs(x) < Mathf.Abs(y))
        {
            if (y > 0)
            {
                animState = AnimationState.WALK_BACK;
            }
            else
            {
                animState = AnimationState.WALK_FRONT;
            }
        }
        else
        {
            animState = AnimationState.WALK_FRONT_SIDE;
        }
        animator.SetInteger("direction", (int)animState);

        spriteRenderer.flipX = x < 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Stop attacking when player is reached
        if (collision.gameObject == player)
        {
            attack = false;
            GameManager.GetInstance().GameOver(EndGameStats.GameOverType.CATCHED_BY_OPPONENT);
        }
    }
}
