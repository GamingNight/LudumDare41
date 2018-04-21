using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public GameObject ball;

    SpriteRenderer spriteRender;
    private float ballCircleColliderRadius;

    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        ballCircleColliderRadius = ball.GetComponent<CircleCollider2D>().radius;
        transform.Rotate(0f, 0f, Random.value * 360f);
        float randomScale = Random.value * 0.3f;
        transform.localScale = new Vector3(1 - randomScale, 1 - randomScale, 1 - randomScale);
    }

    void Update()
    {
        float alpha = Mathf.Min((transform.position - ball.transform.position).magnitude - ballCircleColliderRadius / 7f, ballCircleColliderRadius) / ballCircleColliderRadius;
        spriteRender.color = new Color(1, 1, 1, alpha);

    }
}
