using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableRB : MonoBehaviour
{
    [SerializeField]
    Vector2 forceDirection;

    Vector2 inputDirection;

    [SerializeField]
    float trq;

    Rigidbody2D rb;

    [SerializeField]
    private float lifeTime = 10f;

    // Start is called before the first frame update
    void Start()
    {
        float randTourqe = UnityEngine.Random.Range(-trq, trq);
        float randForceX = UnityEngine.Random.Range(-forceDirection.x, forceDirection.x);
        float randForceY = UnityEngine.Random.Range(-forceDirection.y, forceDirection.y);

        inputDirection.x = randForceX;
        inputDirection.y = randForceY;

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(inputDirection);
        rb.AddTorque(randTourqe);

    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
