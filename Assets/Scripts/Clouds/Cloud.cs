using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public CircleCollider2D safeZone;

    SpriteRenderer spriteRender;
    private float safeZoneRadius;

    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        safeZoneRadius = safeZone.radius;
        transform.Rotate(0f, 0f, Random.value * 360f);
        float randomScale = Random.value * 0.3f;
        transform.localScale = new Vector3(1 - randomScale, 1 - randomScale, 1 - randomScale);
    }

    void Update()
    {
        float alpha = Mathf.Min((transform.position - safeZone.transform.position).magnitude - safeZoneRadius / 20f, safeZoneRadius) / safeZoneRadius;
        spriteRender.color = new Color(1, 1, 1, alpha);
    }
}
