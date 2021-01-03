using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CodeMonkey.MonoBehaviours;
using CodeMonkey.Utils;

public class Bullet : MonoBehaviour
{

    private int bulletElementType;            // 0 - fire, 1 - water, 2 - ice,  
    public float bulletSpeed;
    [SerializeField]
    private float lifeTime = 5f;
    public Rigidbody2D rb;
    private bool playerBullet = false;
    [SerializeField]
    private float damageToDo = 2;

    private CircleCollider2D explosionRadius;
    private SpriteRenderer explosionDrawing;
    public bool isRocket = false;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (isRocket)
        {
            explosionRadius = GetComponentInChildren<CircleCollider2D>();
            explosionDrawing = GetComponentInChildren<SpriteRenderer>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!isRocket)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                EnableExplosion();
                
            }
        }
    }

    public void SetBulletProperties(int type, bool isPlayerHolding)
    {
        this.bulletElementType = type;
        this.playerBullet = isPlayerHolding;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log(collision.tag);
        switch (collision.tag)
        {
            case "Player":
                if (!playerBullet)
                {
                    Debug.Log("hit player");
                }

                break;

            case "Boss":
                if (playerBullet)
                {
                    Debug.Log("hit boss");
                    collision.GetComponent<Boss>().TakeDamage(damageToDo, bulletElementType);
                    Destroy(gameObject);
                }

                break;

        }
    }

    private void EnableExplosion()
    {
        explosionRadius.enabled = true;
        explosionDrawing.enabled = true;
        rb.velocity = Vector3.zero;
        DisableExplosion();
    }

    private void DisableExplosion()
    {
        StartCoroutine(waitingCoroutine());

        

    }

    IEnumerator waitingCoroutine()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
