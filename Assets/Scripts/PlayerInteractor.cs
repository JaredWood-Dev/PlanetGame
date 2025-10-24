using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    
    /*
     * This script allows the player to interact with various interactible objects.
     */
    
    public List<GameObject> currentOverlappingObjects;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactable") && !currentOverlappingObjects.Contains(other.gameObject))
        {
            currentOverlappingObjects.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            currentOverlappingObjects.Remove(other.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (GameObject currentOverlappingObject in currentOverlappingObjects)
            {
                currentOverlappingObject.GetComponent<Interactible>().Interact(gameObject);
            }
        }
    }
}
