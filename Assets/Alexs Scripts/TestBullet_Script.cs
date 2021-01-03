using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet_Script : MonoBehaviour
{
    // Start is called before the first frame update
    private int bulletElementType;            // 0 - fire, 1 - water, 2 - ice,  
    public float bulletSpeed;
    [SerializeField]
    private float lifeTime = 5f;
    public Rigidbody2D rb;
 
    public bool playerBullet = false;
    [SerializeField]
    private float damageToDo = 2;

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetBulletProperties(int type, bool isPlayerHolding)
    {
        this.bulletElementType = type;
        this.playerBullet = isPlayerHolding;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggered: " + collision.name + " " + collision.tag);
        Debug.Log("triggered: " + collision.name);
        switch (collision.tag)
        {
            case "Player":
                if (!playerBullet)
                {
                    Debug.Log("hit player");
                    Destroy(gameObject);
                }

                break;

            case "Boss":
                if (playerBullet)
                {
                    Debug.Log("hit boss");
                    //collision.GetComponent<Boss>().TakeDamage(damageToDo, bulletElementType);
                    Destroy(gameObject);
                }

                break;

            case "Grunt":
                if (playerBullet)
                {
                    Debug.Log("Hit Grunt");
                    collision.GetComponent<Grunt_Script>().TakeDamage(damageToDo, bulletElementType);
                    Destroy(gameObject);
                }

                break;

        }
    }

}
