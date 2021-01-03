using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CodeMonkey.MonoBehaviours;
using CodeMonkey.Utils;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    [SerializeField]
    private float lifeTime = 5f;
    public Rigidbody2D rb;
    private bool playerBullet = false;
    [SerializeField]
    private float damageToDo = 2;


    public GameObject explosion;
    public bool isRocket = false;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (isRocket)
        {
            
            explosion = GameObject.FindGameObjectWithTag("Explosion Radius");
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
                //EnableExplosion();     EXPLOSIONS ARE BUGGED
                Destroy(gameObject);
            }
        }
    }

    public void SetBulletProperties( bool isPlayerHolding)
    {
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
                    collision.GetComponent<Boss>().TakeDamage(damageToDo);
                    Destroy(gameObject);
                }

                break;

        }
    }

    private void EnableExplosion()
    {
        explosion.GetComponent<Collider2D>().enabled = true;
        explosion.GetComponent<DisplayExplosion>().exposionOn();
        rb.velocity = Vector3.zero;
        //DisableExplosion();
    }

    private void DisableExplosion()
    {
        //StartCoroutine(waitingCoroutine());

        

    }

    IEnumerator waitingCoroutine()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
