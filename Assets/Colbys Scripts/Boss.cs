using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.MonoBehaviours;
using CodeMonkey.Utils;
using System;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private GameLogic gameLogic;

    public GameObject[] ModulePrefabs;

   
    public Transform[] lvl1ModPos;
    public Transform[] lvl2ModPos;
    public Transform[] lvl3ModPos;

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

    private Quaternion[] lvl1QuatList;
    private Quaternion[] lvl2QuatList;
    private Quaternion[] lvl3QuatList;

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



    Quaternion lvlTwoQuatOne = Quaternion.Euler(0, 0, 0);
    Quaternion lvlTwoQuatTwo = Quaternion.Euler(0, 0, 270);
    Quaternion lvlTwoQuatThree = Quaternion.Euler(0, 0, 180);
    Quaternion lvlTwoQuatFour = Quaternion.Euler(0, 0, 90);
    
    
    Quaternion lvlThreeQuatOne = Quaternion.Euler(0, 0, 324);
    Quaternion lvlThreeQuatTwo = Quaternion.Euler(0, 0, 252);
    Quaternion lvlThreeQuatThree= Quaternion.Euler(0, 0, 180);
    Quaternion lvlThreeQuatFour = Quaternion.Euler(0, 0, 105);
    Quaternion lvlThreeQuatFive= Quaternion.Euler(0, 0, 38);




    // Start is called before the first frame update
    void Start()
    {
        gameLogic = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>();

        lvl1QuatList = new Quaternion[3];
        lvl2QuatList = new Quaternion[4];
        lvl3QuatList = new Quaternion[5];

        lvl1QuatList[0] = lvlOneQuatOne;
        lvl1QuatList[1] = lvlOneQuatTwo;
        lvl1QuatList[2] = lvlOneQuatThree;

        lvl2QuatList[0] = lvlTwoQuatOne;
        lvl2QuatList[1] = lvlTwoQuatTwo;
        lvl2QuatList[2] = lvlTwoQuatThree;
        lvl2QuatList[3] = lvlTwoQuatFour;

        lvl3QuatList[0] = lvlThreeQuatOne;
        lvl3QuatList[1] = lvlThreeQuatTwo;
        lvl3QuatList[2] = lvlThreeQuatThree;
        lvl3QuatList[3] = lvlThreeQuatFour;
        lvl3QuatList[4] = lvlThreeQuatFive;
     

        health = maxHealth;
        anim = GetComponent<Animator>();
        
        stateTimerHold = stateTimer;
        //moduleList = GameObject.FindGameObjectsWithTag("Module");
   
        switch(gameLogic.Round)
        {
            case 1:
                numberOfModules = lvl1ModPos.Length;
                break;

           case 2:
                numberOfModules = lvl2ModPos.Length;
                break;

            case 3:
                numberOfModules = lvl3ModPos.Length;
                break;
        }
       
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
 
            for (counter = 0; counter <= instantiatedList.Count -1; counter++)
            {
                Destroy(instantiatedList[counter]);
            }
            instantiatedList.Clear();
        }


        moduleList = new GameObject[numberOfModules];

        for(counter = 0; counter <= moduleList.Length - 1; counter++)
        {
            int randVal = UnityEngine.Random.Range(0, numberOfModules - 1);

            moduleList[counter] = ModulePrefabs[randVal];
            moduleList[counter].GetComponent<Module>().SetModuleOnline(false);

            switch (gameLogic.Round)
            {
                case 1:
                    moduleList[counter].transform.position = lvl1ModPos[counter].position;
                    instantiatedList.Add(Instantiate(moduleList[counter], lvl1ModPos[counter].position, lvl1QuatList[counter], lvl1ModPos[counter].transform));
                    break;

                case 2:
                    moduleList[counter].transform.position = lvl2ModPos[counter].position;
                    instantiatedList.Add(Instantiate(moduleList[counter], lvl2ModPos[counter].position, lvl2QuatList[counter], lvl2ModPos[counter].transform));
                    break;

                case 3:
                    moduleList[counter].transform.position = lvl3ModPos[counter].position;
                    instantiatedList.Add(Instantiate(moduleList[counter], lvl3ModPos[counter].position, lvl3QuatList[counter], lvl3ModPos[counter].transform));
                    break;
            }
            

            
            

            

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

            switch(gameLogic.Round)
            {
                case 1:
                    for (counter = 0; counter <= lvl1QuatList.Length - 1; counter++)
                    {
                        lvl1QuatList[counter] = lvl1ModPos[counter].rotation;
                    }
                    break;

                case 2:
                    for (counter2 = 0; counter2 <= lvl2QuatList.Length - 1; counter2++)
                    {
                        lvl2QuatList[counter2] = lvl2ModPos[counter2].rotation;
                    }
                    break;

                case 3:
                    for (counter = 0; counter <= lvl3ModPos.Length - 1; counter++)
                    {
                        lvl3QuatList[counter] = lvl3ModPos[counter].rotation;
                    }
                    break;

                case 4:

                    break;
            }
            
            
            

            if (bossAwake)
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

