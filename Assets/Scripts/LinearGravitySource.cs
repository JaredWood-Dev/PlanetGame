using Unity.VisualScripting;
using UnityEngine;
public class LinearGravitySource : GravitySource
{
    public Vector2 fieldDirection = new Vector2(0, -1);
    public bool hideIndicator = true;

    void Start()
    {
        //Convert the Collider's Rotation into the Gravity Direction
        
        
        if (GetComponent<SpriteRenderer>() && hideIndicator)
            GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        fieldDirection = transform.right.normalized;
    }
}