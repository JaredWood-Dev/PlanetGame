using UnityEngine;

public class Checkpoint : Interactable
{
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    public override void Interact()
    {
        EventManager.CheckPointUpdate(transform.position);
        GetComponent<SpriteRenderer>().sprite = activeSprite;
    }

    public void DisableCheckPoint(Vector2 position)
    {
        //return sprite to normal
        GetComponent<SpriteRenderer>().sprite = inactiveSprite;
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
