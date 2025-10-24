using UnityEngine;

public class Interactible : MonoBehaviour
{
    public virtual void Interact(GameObject player)
    {
        print(player + "interacted!");
    }
}
