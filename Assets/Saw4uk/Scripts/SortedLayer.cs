using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortedLayer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float offset;
    [SerializeField] private bool isStatic;

    private void Awake()
    {
        if (isStatic)
        {
            spriteRenderer.sortingOrder = -(int)(spriteRenderer.transform.position.y + offset) + 100;
            Destroy(this);
        }
    }

    private void Update()
    {
        spriteRenderer.sortingOrder = -(int)(spriteRenderer.transform.position.y + offset) + 100;
    }
}
