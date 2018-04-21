using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    SpriteRenderer spriteRender;
    bool lerp = false;
    public float lerpDuration = 1;
    private float lerpTime = 0;


    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (lerp)
        {
            lerpTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, lerpTime / lerpDuration);
            spriteRender.color = new Color(1, 1, 1, alpha);
            if (lerpTime >= 1)
            {
                lerpTime = 0;
                lerp = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ball")
        {
            lerp = true;
        }
    }
}
