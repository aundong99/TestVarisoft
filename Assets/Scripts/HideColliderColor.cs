using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HideColliderColor : MonoBehaviour
{
    private TilemapRenderer tilemapRenderer;

    private void Awake()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
    }
    private void Start()
    {
        tilemapRenderer.enabled = false;
    }

    
}
