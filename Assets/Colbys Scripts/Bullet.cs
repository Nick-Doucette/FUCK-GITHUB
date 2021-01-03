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
            lifeTime = 3f;
            explosion = GameObject.FindGameObjectWithTag("Explosion Radius");
            SoundManager.PlaySound(SoundManager.Sound.RocketFire, transform.position);
        }
        else
        {
            SoundManager.PlaySound(SoundManager.Sound.GunFire, transform.position);
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
                //EnableExplosion();    // EXPLOSIONS ARE BUGGED
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
                    SoundManager.PlaySound(SoundManager.Sound.PlayerDead, transform.position);
                    Debug.Log("hit player");
                }

                break;

            case "Boss":
                if (playerBullet)
                {
                    SoundManager.PlaySound(SoundManager.Sound.EnemyHit, transform.position);
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
        SoundManager.PlaySound(SoundManager.Sound.Explosion, transform.position);
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
