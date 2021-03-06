﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLayerManager : MonoBehaviour {

    private List<SpriteRenderer> onFieldSprites;
    private SpriteRenderer[] orderedSprites;

    private void Start() {

        onFieldSprites = new List<SpriteRenderer>();
    }

    void Update() {

        onFieldSprites.Clear();
        SpriteRenderer[] allSprites = FindObjectsOfType<SpriteRenderer>();
        foreach (SpriteRenderer sprite in allSprites) {
            if (sprite.sortingLayerName == "OnFieldObject")
                onFieldSprites.Add(sprite);
        }

        orderedSprites = new SpriteRenderer[onFieldSprites.Count];
        for (int i = 0; i < onFieldSprites.Count; i++) {
            SpriteRenderer sprite = onFieldSprites[i];
            int j = 0;
            bool ordered = false;
            SpriteRenderer temp = null;
            while (j < i) {
                SpriteRenderer spriteToCompare = temp == null ? sprite : temp;
                float orderedSize = orderedSprites[j].transform.position.y - orderedSprites[j].bounds.extents.y;
                float sizeToCompare = spriteToCompare.transform.position.y - spriteToCompare.bounds.extents.y;
                if (orderedSize < sizeToCompare) {
                    temp = orderedSprites[j];
                    orderedSprites[j] = spriteToCompare;
                    ordered = true;
                }
                j++;
            }
            if (!ordered) {
                orderedSprites[i] = sprite;
            } else {
                orderedSprites[i] = temp;
            }
        }

        for (int i = 0; i < orderedSprites.Length; i++) {
            orderedSprites[i].sortingOrder = i;
        }
    }
}
