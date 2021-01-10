﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CodeMonkey.MonoBehaviours;
using CodeMonkey.Utils;


public class Module : MonoBehaviour
{

    private Transform playerPosition;

    [SerializeField]
    private float rateOfFire;
    private float rateOfFireTimer;
    
    public bool shooting;
    [SerializeField]
    private bool isPlayerHolding = false;

    public bool isAttachedToBoss = true;
    public GameObject bullet;

    private Vector3 aimPoint;

    private Vector2 directionToShoot;
    private Vector3 burstDirectionToShoot;

    public bool shieldModule;

    public bool burstModule;

    public bool moduleOnline = false;

    public bool gunModule;
    [SerializeField]
    public bool isClicking;
    private Action shootingAction;

    private Boss boss;

    public GameObject pc;

    private GameLogic gl;

    private GameObject[] bulletBurstList;
    public GameObject[] tranformBurstList;

    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        gl = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
        pc = GameObject.FindGameObjectWithTag("ourPlayer");
        playerPosition = pc.transform;

        //tranformBurstList = GameObject.FindGameObjectsWithTag("Burst Direction");

       // boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();

        if (gunModule)
        {

            rateOfFireTimer = rateOfFire;
            shootingAction = ShootFunction;
        }

        //-------------------------------
        else if(shieldModule)
        {
           // moduleElementType = 0;
            

            boss.EditDamageMod(.5f);
        }
        else if(burstModule)
        {
            shootingAction = BurstFunction;
        }
       
    }



    // Update is called once per frame
    void Update()
    {
        if(!isAttachedToBoss && !isPlayerHolding)
        {
            moduleOnline = false;
        }
        if (!shieldModule)
        {
            if (isPlayerHolding && !shieldModule)
            {
                aimPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                aimPoint = new Vector3(aimPoint.x, aimPoint.y, 0);
            }
            else if(!isPlayerHolding && !shieldModule)
            {
                aimPoint = playerPosition.position;
                
            }

            if (isClicking && pc.GetComponent<Player_Control>().playerCanMove)
            {
                //Debug.Log("Clicking");
                shooting = true;
            }
            else
            {
                shooting = false;
            }
            if(!isPlayerHolding && !shieldModule)
            {
                shooting = true;
            }

            if (shooting && moduleOnline)
            {
                rateOfFireTimer -= Time.deltaTime;
                if(rateOfFireTimer <= 0)
                {
                    shootingAction();
                    rateOfFireTimer = rateOfFire;
                }
                

            }
        }
     

        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    private void ShootFunction()
    {

        GameObject holdBullet = Instantiate(bullet,transform.position,Quaternion.identity);
        holdBullet.GetComponent<Bullet>().SetBulletProperties(isPlayerHolding);
        directionToShoot = aimPoint - transform.position;
        directionToShoot = directionToShoot.normalized;
        holdBullet.GetComponent<Rigidbody2D>().velocity = directionToShoot * holdBullet.GetComponent<Bullet>().bulletSpeed;
        holdBullet.transform.SetParent(null);
        //Debug.Log("shooting element type: " + moduleElementType);
    }

    private void BurstFunction()
    {

        bulletBurstList = new GameObject[8];

        rateOfFire = 2f;
        for(counter = 0; counter <= bulletBurstList.Length -1; counter++)
        {
            bulletBurstList[counter] = Instantiate(bullet,transform.position,Quaternion.identity);
            bulletBurstList[counter].GetComponent<Bullet>().SetBulletProperties(isPlayerHolding);
            //bulletBurstList[counter].GetComponent<Bullet>().transform.rotation = Quaternion.Euler(bulletBurstList[counter].GetComponent<Bullet>().rb.transform.up);
            directionToShoot = tranformBurstList[counter].transform.position - transform.position;
            directionToShoot = directionToShoot.normalized;

            bulletBurstList[counter].GetComponent<Rigidbody2D>().velocity = directionToShoot * bulletBurstList[counter].GetComponent<Bullet>().bulletSpeed;


        }
        


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isAttachedToBoss && !isPlayerHolding)
        {
            SoundManager.PlaySound(SoundManager.Sound.Selection, transform.position);
            gl.addScore(10);
            Destroy(gameObject);
        }
    }

    public void SetModuleOnline(bool online)
    {
        this.moduleOnline = online;
    }

    private void OnDrawGizmos()
    {
        if (!isPlayerHolding && !shieldModule)
        {
            Gizmos.DrawWireSphere(aimPoint, 2f);
        }
            
    }
}
