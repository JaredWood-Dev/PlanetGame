using UnityEngine;

public class Interactable : MonoBehaviour
{
    /*
     * This component allows an object to be interacted with when a plaver is close enough an presses an interact key on the object.
     * Specific implementations will inherit from this component for various objects:
     * - Doors
     * - Buttons
     * - Teleporters
     * - Shops
     */
    
    public Sprite highlight;
    public bool isClose;
    protected Sprite DefaultSprite;
    private bool _isPressed;

    void Start()
    {
        DefaultSprite = GetComponent<SpriteRenderer>().sprite;
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (isClose && !_isPressed)
            {
                Interact();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isClose = true;
            GetComponent<SpriteRenderer>().sprite = highlight;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isClose = false;
            GetComponent<SpriteRenderer>().sprite = DefaultSprite;
        }
    }

    public virtual void Interact()
    {
        print("Interact");
        _isPressed = true;
    }
}
