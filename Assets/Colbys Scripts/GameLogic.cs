using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{

    public int Round;
    public Boss boss;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
        //Round = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(boss.GetIsDead())
        {
            switch(Round)
            {
                case 1:
                    SceneManager.LoadScene("Colbys Second Scene");
                    break;
            }
            
        }
    }
}
