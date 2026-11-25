using System;
using UnityEngine;

public class WrapTrigger : MonoBehaviour
{
    public bool isVertical = true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the triggering object is a player
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject otherWarp = null;
            for (int i = 0; i < transform.parent.childCount; i++)
            {
                if (transform.parent.GetChild(i).gameObject != gameObject)
                {
                    otherWarp = transform.parent.GetChild(i).gameObject;
                }
            }
            
            otherWarp.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            if (otherWarp != null)
            {
                if (isVertical)
                {
                    other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x,
                        otherWarp.transform.position.y, other.gameObject.transform.position.z);
                    Camera.main.transform.position = new Vector2(other.transform.position.x, other.transform.position.y);
                }
                else
                {
                    other.gameObject.transform.position = new Vector3(otherWarp.gameObject.transform.position.x,
                        other.gameObject.transform.position.y, other.gameObject.transform.position.z);
                    Camera.main.transform.position = new Vector2(other.transform.position.x, other.transform.position.y);
                }
            }
            
            Invoke(nameof(EnableWarp), 1f);
        }
    }

    void EnableWarp()
    {
            GameObject otherWarp = null;
            for (int i = 0; i < transform.parent.childCount; i++)
            {
                if (transform.parent.GetChild(i).gameObject != gameObject)
                {
                    otherWarp = transform.parent.GetChild(i).gameObject;
                }
            }
            
            otherWarp.GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = true;
    }
}

