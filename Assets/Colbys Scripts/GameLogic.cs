using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{

    public GameObject nextLevel;
    public int Round;
    public GameObject boss;
    public GameObject boss2;
    public GameObject boss3;
    public GameObject boss4;
    public Transform spawnPoint;

    public GameObject[] nextLevelPoints;

    public bool onNextLevel = false;
    private bool death = false;

    private int ourScore = 0;

    private Text scoreText;

    int counter = 0;
    int RandValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();


        for(counter = 0; counter <= nextLevelPoints.Length - 1; counter++)
        {
            nextLevelPoints[counter].gameObject.SetActive(false);
        }
        

        //DontDestroyOnLoad(this.gameObject);

        boss = GameObject.FindGameObjectWithTag("Boss");

        setRandomSpotVisible();

    }

    private void setRandomSpotVisible()
    {
        for (counter = 0; counter <= nextLevelPoints.Length - 1; counter++)
        {
            nextLevelPoints[counter].gameObject.SetActive(false);
        }

        int RandValue2 = Random.Range(0, 4);

        nextLevelPoints[RandValue2].gameObject.SetActive(true);
    }

    public void addScore(int ScoreToAdd)
    {
        ourScore += ScoreToAdd;
        scoreText.text = ourScore.ToString();
    }


    // Update is called once per frame
    void Update()
    {

        for (counter = 0; counter <= nextLevelPoints.Length - 1; counter++)
        {


            if (boss.GetComponent<Boss>().GetIsDead() && !death && nextLevelPoints[counter].GetComponent<NextLevel>().standingOn)
            {

                setRandomSpotVisible();

                death = true;

                Round++;
                switch (Round)
                {
                    case 2:
                        boss = Instantiate(boss2, spawnPoint, true);
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

                    default:
                        boss = Instantiate(boss, spawnPoint, true);
                        break;
                }
            }
        }

    }
}
