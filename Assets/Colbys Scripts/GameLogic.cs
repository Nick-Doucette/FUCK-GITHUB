using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{

    public GameObject nextLevel;
    public int Round;
    public GameObject boss;
    public GameObject boss2;
    public GameObject boss3;
    public GameObject boss4;
    public Transform spawnPoint;

    public Transform[] nextLevelPoints;

    public bool onNextLevel = false;
    private bool death = false;
    // Start is called before the first frame update

    void Awake()
    {
        SoundManager.Initialize();
    }
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        boss = GameObject.FindGameObjectWithTag("Boss");

        int RandValue = Random.Range(0, 4);

        nextLevel.GetComponent<Transform>().position = nextLevelPoints[RandValue].position;

    }

    // Update is called once per frame
    void Update()
    {
        
        if(boss.GetComponent<Boss>().GetIsDead() && !death && onNextLevel)
        {
            int RandValue = Random.Range(0, 4);

            nextLevel.GetComponent<Transform>().position = nextLevelPoints[RandValue].position;

            Debug.Log("Dead");
            death = true;
       
            Round++;
            switch(Round)
            {
                case 2:
                    boss = Instantiate(boss2, spawnPoint,true);
                    death = false;
                    break;

                case 3:
                    boss = Instantiate(boss3, spawnPoint, true);
                    death = false;
                    break;

                case 4:
                    boss = Instantiate(boss4, spawnPoint, true);
                    death = false;
                    break;
            }
        }

    }
}
