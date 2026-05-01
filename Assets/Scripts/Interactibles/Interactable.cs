using UnityEngine;
using UnityEngine.InputSystem;

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
    protected GameObject player;

    [SerializeField]
    private InputActionReference interactAction;

    void Start()
    {
        //DefaultSprite = GetComponent<SpriteRenderer>().sprite;
        
        //interactAction = InputSystem.actions.FindAction("Player/Interact");
    }

    void Update()
    {
        if (interactAction.action.WasPressedThisFrame())
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
            //GetComponent<SpriteRenderer>().sprite = highlight;
            player = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isClose = false;
            //GetComponent<SpriteRenderer>().sprite = DefaultSprite;
            player = null;
        }
    }

    public virtual void Interact()
    {
        print("Interact");
        _isPressed = true;
    }
}
