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
    public Transform[] lvl4ModPos;

    private int numberOfModules;
    [SerializeField]
    public float health;
    [SerializeField]
    public float maxHealth;

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
    private Quaternion[] lvl4QuatList;

    [SerializeField]
    private float DamageModifier = 1f;

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

    private BreakAppart destructableThing; 

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
    
    Quaternion lvlFourQuatOne= Quaternion.Euler(0, 0, 302);
    Quaternion lvlFourQuatTwo= Quaternion.Euler(0, 0, 240);
    Quaternion lvlFourQuatThree= Quaternion.Euler(0, 0, 180);
    Quaternion lvlFourQuatFour= Quaternion.Euler(0, 0, 122);
    Quaternion lvlFourQuatFive= Quaternion.Euler(0, 0, 60);
    Quaternion lvlFourQuatSix= Quaternion.Euler(0, 0, 0);


    int checkForShields = 0;

    // Start is called before the first frame update
    void Start()
    {
        destructableThing = GetComponentInChildren<BreakAppart>();

        gameLogic = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>();

        lvl1QuatList = new Quaternion[3];
        lvl2QuatList = new Quaternion[4];
        lvl3QuatList = new Quaternion[5];
        lvl4QuatList = new Quaternion[6];

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
     
        lvl4QuatList[0] = lvlFourQuatOne;
        lvl4QuatList[1] = lvlFourQuatTwo;
        lvl4QuatList[2] = lvlFourQuatThree;
        lvl4QuatList[3] = lvlFourQuatFour;
        lvl4QuatList[4] = lvlFourQuatFive;
        lvl4QuatList[5] = lvlFourQuatSix;

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

            case 4: 
                numberOfModules = lvl4ModPos.Length;
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
            int randVal = UnityEngine.Random.Range(0, 4);

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

                case 4:
                    moduleList[counter].transform.position = lvl4ModPos[counter].position;
                    instantiatedList.Add(Instantiate(moduleList[counter], lvl4ModPos[counter].position, lvl4QuatList[counter], lvl4ModPos[counter].transform));
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
            for(counter = 0; counter <= instantiatedList.Count -1; counter++)
            {
                if (!instantiatedList[counter].GetComponent<Module>().shieldModule)
                {
                    checkForShields++;
                }
            }
            if(checkForShields == instantiatedList.Count)
            {
                DamageModifier = 1f;
                checkForShields = 0;
            }
            else
            {
                checkForShields = 0;
            }

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
                    for (counter = 0; counter <= lvl4ModPos.Length - 1; counter++)
                    {
                        lvl4QuatList[counter] = lvl4ModPos[counter].rotation;
                    }
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
               // destructableThing.ExplodeThisGameObject();
            }

        }
        
    }



    public void EditDamageMod(float damageModifier)
    {

        DamageModifier = damageModifier;
            
    }

    public void TakeDamage(float amountOfDamage)
    {
   
        amountOfDamage = amountOfDamage * DamageModifier;
        health -= amountOfDamage;
              
    }

    private void StartBoss()
    {
        anim.SetBool("isFollowing", true);
        bossAwake = true;
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

    private void ExplodeTheBoss()
    {
        //GameObject destructable = (GameObject) Instantiate
        //GameObject destructable = (GameObject) Instantiate
    }


}

