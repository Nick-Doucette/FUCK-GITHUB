using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{

    public BoxCollider2D nextLevel;
    public int Round;
    public GameObject boss;
    public GameObject boss2;
    public GameObject boss3;
    public GameObject boss4;
    public Transform spawnPoint;
    private bool death = false;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        boss = GameObject.FindGameObjectWithTag("Boss");

    }

    // Update is called once per frame
    void Update()
    {
        
        if(boss.GetComponent<Boss>().GetIsDead() && !death)
        {
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
