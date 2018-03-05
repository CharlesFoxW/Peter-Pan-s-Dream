using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class donghua : MonoBehaviour {

    // Use this for initialization
    public Sprite[] Sprites;
    public float speed;
    private SpriteRenderer spriterenderer;

    void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();

    }
    void Update()
    {
        int index = (int)(Time.time * speed) % Sprites.Length;
        spriterenderer.sprite = Sprites[index];
    }
}
