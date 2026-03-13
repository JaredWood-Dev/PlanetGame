using UnityEngine;

public class Collectible : MonoBehaviour
{
    // An object that has this component can be collected by the player.

    void OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger Collided");
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
