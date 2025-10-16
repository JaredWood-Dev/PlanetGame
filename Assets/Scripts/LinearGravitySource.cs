using Unity.VisualScripting;
using UnityEngine;
public class LinearGravitySource : GravitySource
{
    public Vector2 fieldDirection = new Vector2(0, -1);

    void Start()
    {
        float rot = transform.eulerAngles.z;
        //Convert the Collider's Rotation into the Gravity Direction
        fieldDirection = -new Vector2(Mathf.Cos(Mathf.Deg2Rad * rot), Mathf.Sin(Mathf.Deg2Rad * rot));
    }
}