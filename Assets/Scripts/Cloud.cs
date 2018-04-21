using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    SpriteRenderer spriteRender;
    bool lerpIn = false;
    bool lerpOut = false;
    public float lerpDuration = 1;
    private float lerpTime = 0;


    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (lerpIn)
        {
            lerpTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, lerpTime / lerpDuration);
            spriteRender.color = new Color(1, 1, 1, alpha);
            if (lerpTime >= 1)
            {
                lerpTime = 0;
                lerpIn = false;
            }
        }

        if (lerpOut)
        {
            lerpTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, lerpTime / lerpDuration);
            spriteRender.color = new Color(1, 1, 1, alpha);
            if (lerpTime >= 1)
            {
                lerpTime = 0;
                lerpOut = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ball")
        {
            lerpIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        lerpOut = true;
    }
}
