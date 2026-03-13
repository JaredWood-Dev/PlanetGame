using System;
using UnityEngine;

public class Door : TriggerObject
{
    public Sprite openSprite;
    public bool isOpen;
    private Sprite _initalSprite;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _initalSprite = _spriteRenderer.sprite;
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    public override void Trigger()
    {
        print("door triggered");
        if (!isOpen)
        {
            _spriteRenderer.sprite = openSprite;
            _boxCollider.isTrigger = true;
            isOpen = true;
        }
        else
        {
            _spriteRenderer.sprite = _initalSprite;
            _boxCollider.isTrigger = false;
            isOpen = false;
        }
    }
}
