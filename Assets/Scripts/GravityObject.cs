using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class GravityObject : MonoBehaviour
{
    /*
     * This script enables an object to be effected by gravity field(s). These gravity fields modify the 'gravity' property of the rigidbody.
     */

    public Vector2 downDirection;
    public float gravity;
    public GameObject gravityObject;
    public GravityType gravityType;
    public float rotationSpeed = 0.25f;
    
    public List<Collider2D> gravityColliders;
    
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //_rb.AddForce(downDirection * (gravity * Time.fixedTime), ForceMode2D.Force);
        _rb.linearVelocity += downDirection * gravity * Time.fixedDeltaTime;
        Debug.DrawRay(transform.position, downDirection, Color.red);

        if (gravityObject)
        {
            switch (gravityType)
            {
                case GravityType.Radial:
                    downDirection = (gravityObject.transform.position - gameObject.transform.position).normalized;
                    break;
                case GravityType.Linear:
                    downDirection = gravityObject.GetComponent<LinearGravitySource>().fieldDirection;
                    break;
                case GravityType.Dynamic:
                    break;
            }
            
            //gameObject.transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(downDirection.y, downDirection.x) * 180 / Mathf.PI) + 90);
            var currentUp = transform.up;
            var targetUp = -downDirection;
            //transform.up = -downDirection;
            transform.up = Vector2.Lerp(currentUp, targetUp,rotationSpeed);
        }
    }

    public void SetGravityDirection(Vector2 dir)
    {
        downDirection = dir;
    }

    public void CheckGravitySources()
    {
        if (gravityColliders.Count > 0)
        {
            Collider2D field = gravityColliders[0];
            int higestPriority = 0;
            foreach (var gravityField in gravityColliders)
            {
                if (gravityField.GetComponent<GravitySource>().gravityPriority > higestPriority)
                {
                    field = gravityField;
                    higestPriority = gravityField.GetComponent<GravitySource>().gravityPriority;
                }
            }
            
            gravityObject = field.gameObject;
            gravityType = field.gameObject.GetComponent<GravitySource>().gravityType;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        CheckGravitySources();
        
        if (other.gameObject.GetComponent<GravitySource>() != null)
            gravityColliders.Add(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        CheckGravitySources();
        if (other.gameObject.GetComponent<GravitySource>() != null)
            gravityColliders.Remove(other);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        CheckGravitySources();
        if (other.gameObject.GetComponent<GravitySource>() != null && !gravityColliders.Contains(other))
            gravityColliders.Add(other);
    }
}
