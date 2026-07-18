using System;
using UnityEngine;

public class Checkpoint : Interactable
{
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    public bool isActive;
    [SerializeField]
    private Animator a;
    
    public override void Interact()
    {
        EventManager.CheckPointUpdate(transform.position);
        a.SetTrigger("activate");
        isActive = true;
        
    }

    public void DisableCheckPoint(Vector2 position)
    {
        //return sprite to normal
        if (!isActive)
            return;
        
        print(position);
        //dont disable the active checkpoint!
        if (position == (Vector2)transform.position)
            return;
        a.SetTrigger("deactivate");
        isActive = false;
    }

    void OnEnable()
    {
        EventManager.OnCheckPointUpdate += DisableCheckPoint;
    }

    void OnDisable()
    {
        EventManager.OnCheckPointUpdate -= DisableCheckPoint;
    }
}
