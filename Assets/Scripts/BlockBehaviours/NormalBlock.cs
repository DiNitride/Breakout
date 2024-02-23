using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBlock : BlockBehaviour
{      
    private SpriteRenderer spriteRenderer;

    [Range(0,1)] public int strength;
    private Color[] strengthColors = new Color[] {
        new Color(1f, 0f, 1f),
        new Color(0.47f, 0f, 1f)
    };

    protected override void Start() {
        base.Start();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void UpdateColor() {
        spriteRenderer.color = strengthColors[strength];
    }

    public override void Trigger() {
        if (strength == 0) {
            levelController.DestroyBlock(x, y); // Destory
            TriggerPowerup();
        } else {
            strength -= 1;
            UpdateColor();
        }
        
    }
}
