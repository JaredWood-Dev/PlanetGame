using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCController : MonoBehaviour
{
    /*
     * This script is a placeholder for a basic wandering NPC controller.
     * It will need to be refactored with a new NPC system.
     */

    //The point in which the wanderable area is centered around
    public Vector2 originPoint;
    
    //The radius in which the NPC can wander away from the origin point
    public float radius = 5;

    //How frequently the NPC chooses a new wander target
    public float frequency = 1;

    //Speed that the object moves at
    public float speed = 1;

    private Rigidbody2D _rb;
    private GravityObject _go;
    private SpriteRenderer _sr;
    private Animator _a;

    void Start()
    {
        originPoint = transform.position;
        
        _rb = GetComponent<Rigidbody2D>();
        _go = GetComponent<GravityObject>();
        _sr = GetComponent<SpriteRenderer>();
        _a = GetComponent<Animator>();
        
        InvokeRepeating(nameof(Wander), 0f, frequency);
    }

    private void Update()
    {
        _a.SetFloat("move", Mathf.Abs(_rb.linearVelocity.x));
    }

    private void Wander()
    {
        _rb.AddForce((ChooseLocation() - originPoint) * speed, ForceMode2D.Impulse);
    }
    
    private Vector2 ChooseLocation()
    {
        float randomX = Random.Range(-radius, radius);
        
        _sr.flipX = !(randomX > 0);
        
        Vector2 newPoint = originPoint + new Vector2(randomX, 0f);
        
        return newPoint;
    }
}
