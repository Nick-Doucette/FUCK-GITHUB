using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBounce : MonoBehaviour
{

    private Rigidbody2D rb;
    Vector3 lastVelocity;
    Vector3 directionOfBounce;
    

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rb.velocity;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
      
        var speed = lastVelocity.magnitude;
        var directionOfBounce = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        rb.velocity = directionOfBounce * Mathf.Max(speed, 0);
    
    }
}
