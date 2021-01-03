using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.MonoBehaviours;
using CodeMonkey.Utils;
using System;

public class Boss : MonoBehaviour
{
    public GameObject[] ModulePrefabs;

   
    public Transform[] lvl1ModPos;


    private int numberOfModules;
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;

    public bool deathTrigger = false;
    private bool bossAwake = false;

    public GameObject item1;
    private Vector2 randomVector;
    private Animator anim;

    [SerializeField]
    private GameObject[] moduleList;

    private List<GameObject> instantiatedList;

    private Quaternion[] quatList;

    private Vector3[] vecList;

    private float waterDamageModifier = 1f;
    private float iceDamageModifier = 1f;
    private float fireDamageModifier = 1f;
    [SerializeField]
    private float stateTimer = 5f;
    [SerializeField]
    private float stateTimerHold = 0;
    [SerializeField]
    private float strightLineSpeed = 2f;

    private Action idleToStartState;

    private Rigidbody2D rb;

    [SerializeField]
    private Vector3 moveDirection;

    Vector2 wallNormal;
    Vector2 bounceDirection;
    int counter = 0;
    int counter2 = 0;

    Transform[] tList;

    // A rotation 300 degrees around the z-axis
    Quaternion lvlOneQuatOne = Quaternion.Euler(0, 0, 300);
    // A rotation 180 degrees around the z-axis
    Quaternion lvlOneQuatTwo = Quaternion.Euler(0, 0, 180);
    // A rotation 60 degrees around the z-axis
    Quaternion lvlOneQuatThree = Quaternion.Euler(0, 0, 60);

    // Start is called before the first frame update
    void Start()
    {
        quatList = new Quaternion[3];
        vecList = new Vector3[3];

        vecList[0] = lvl1ModPos[0].rotation.eulerAngles;
        vecList[1] = lvl1ModPos[1].rotation.eulerAngles;
        vecList[2] = lvl1ModPos[2].rotation.eulerAngles;

        quatList[0] = lvlOneQuatOne;
        quatList[1] = lvlOneQuatTwo;
        quatList[2] = lvlOneQuatThree;

        health = maxHealth;
        anim = GetComponent<Animator>();
        
        stateTimerHold = stateTimer;
        //moduleList = GameObject.FindGameObjectsWithTag("Module");
   
        numberOfModules = lvl1ModPos.Length;
        instantiatedList = new List<GameObject>();
        SetRandomBossModules();

        

        Debug.Log("numberofMod: " + numberOfModules);

        rb = GetComponent<Rigidbody2D>();
        idleToStartState = StartBoss;
        FunctionTimer.Create(idleToStartState, 1f);
        
        moveDirection = UtilsClass.GetRandomDir();
    }

    private void ChangeOnlineModules()
    {
        for(counter = 0; counter <= instantiatedList.Count -1; counter++)
        {
            instantiatedList[counter].GetComponent<Module>().SetModuleOnline(false);
        }

        int amountToOnline = numberOfModules / 2;
        int moduleStartIndex = UnityEngine.Random.Range(0, numberOfModules);

        for (counter = 0; counter <= amountToOnline; counter++)
        {
            moduleStartIndex++;

            if (moduleStartIndex > numberOfModules - 1)
            {
                moduleStartIndex = 0;
            }
            instantiatedList[moduleStartIndex].GetComponent<Module>().SetModuleOnline(true);
        }

            
    }

    private void SetRandomBossModules()
    {

        if(instantiatedList.Count != 0)
        {
            tList = new Transform[numberOfModules];

            for (counter = 0; counter <= instantiatedList.Count - 1; counter++)
            {
                tList[counter] = instantiatedList[counter].transform;
            }
            for (counter = 0; counter <= instantiatedList.Count -1; counter++)
            {
                Destroy(instantiatedList[counter]);
            }
            instantiatedList.Clear();
        }
        else
        {
            tList = new Transform[0];
        }

        moduleList = new GameObject[numberOfModules];

        for(counter = 0; counter <= moduleList.Length - 1; counter++)
        {
            int randVal = UnityEngine.Random.Range(0, numberOfModules);

            moduleList[counter] = ModulePrefabs[randVal];
            moduleList[counter].GetComponent<Module>().SetModuleOnline(false);
            moduleList[counter].transform.position = lvl1ModPos[counter].position;

            
            instantiatedList.Add(Instantiate(moduleList[counter], lvl1ModPos[counter].position, quatList[counter], lvl1ModPos[counter].transform));

            

        }

        counter = 0;
        ChangeOnlineModules();
    }

   
    // Update is called once per frame
    void Update()
    {
        if(!deathTrigger)
        {
            transform.Rotate(new Vector3(0, 0, 1), 50f * Time.deltaTime);

            for(counter = 0; counter <= quatList.Length - 1; counter++)
            {
                quatList[counter] = lvl1ModPos[counter].rotation;
            }

            if(bossAwake)
            {
                stateTimerHold -= Time.deltaTime;

                if (stateTimerHold <= 0)
                {
                    // change the state
                    SetRandomBossModules();

                    stateTimerHold = stateTimer;
                }
            }
            

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                randomVector = UtilsClass.GetRandomDir();
                GameObject holdItem = Instantiate(item1, transform, false);
                holdItem.GetComponent<Rigidbody2D>().AddForce(randomVector * 1800f);
            }


            if (health <= 0)
            {
                anim.SetBool("isDead", true);
                for (counter = 0; counter <= instantiatedList.Count - 1; counter++)
                {
                    instantiatedList[counter].GetComponent<Module>().SetModuleOnline(false);
                    instantiatedList[counter].GetComponent<Module>().isAttachedToBoss = false;
                    instantiatedList[counter].transform.SetParent(null);


                    instantiatedList[counter].tag = "Player Module";
                }
                deathTrigger = true;
            }



        }
        
    }



    public void EditDamageMod(float damageModifier, int elementType)
    {
        switch(elementType)
        {
            case 0:
                fireDamageModifier = damageModifier;
                waterDamageModifier = 2f;
                iceDamageModifier = 1f;
                break;

            case 1:
                waterDamageModifier = damageModifier;
                iceDamageModifier = 2f;
                fireDamageModifier = 1f;
                break;

            case 2:
                iceDamageModifier = damageModifier;
                fireDamageModifier = 2f;
                waterDamageModifier = 1f;
                break;
        }
        
    }

    public void TakeDamage(float amountOfDamage, int typeOfDamage)
    {
        

        switch(typeOfDamage)
        {
            case 0:
                Debug.Log("boss is taking Fire Damage");
                amountOfDamage = amountOfDamage * fireDamageModifier;
                health -= amountOfDamage;
                break;

            case 1:
              //  Debug.Log("boss is taking Water Damage");
                amountOfDamage = amountOfDamage * waterDamageModifier;
                health -= amountOfDamage;
               // Debug.Log(health);
                break;

            case 2:
              //  Debug.Log("boss is taking Ice Damage");
                amountOfDamage = amountOfDamage * iceDamageModifier;
                health -= amountOfDamage;
                break;
        }
    }

    private void StartBoss()
    {
        anim.SetBool("isFollowing", true);
        bossAwake = true;
    }

   public void StrightLineUpdate()
   {
        rb.velocity = moveDirection * strightLineSpeed;
        Debug.Log("rb.velocity: " + rb.velocity);
   }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            wallNormal = collision.contacts[0].normal;
            moveDirection = Vector2.Reflect(rb.velocity, wallNormal).normalized;

            //moveDirection = bounceDirection;

            rb.velocity = moveDirection * strightLineSpeed;
        }
    }

    public void OfflineModules()
    {
        
        for(counter = 0; counter <= moduleList.Length - 1; counter++)
        {
            if(!deathTrigger)
            {
                moduleList[counter].GetComponent<Module>().SetModuleOnline(false);
            }
           
        }
    }

    public bool GetIsDead()
    {
        return deathTrigger;
    }

    private void PopulateModuleList()
    {
        for(counter = 0; counter <= numberOfModules; counter++)
        {
            
        }
    }

}

