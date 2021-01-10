using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using CodeMonkey.Utils;
using CodeMonkey.MonoBehaviours;

public class TutorialLogic : MonoBehaviour
{
    private bool bossDead = false;
    private bool paused = true;
    public int progressCounter = 0;
    public string[] tutorialMessages;
    public string[] whatDoToMessages;
    private Text tutorialText;
    private Text whatToDoText;
    private GameLogic gl;
    private bool trigger = true;
    private Action delayKilledBoss;

    // Start is called before the first frame update
    void Start()
    {
        delayKilledBoss = KilledBoss;

        tutorialText = GameObject.FindGameObjectWithTag("TutorialText").GetComponent<Text>();
        whatToDoText = GameObject.FindGameObjectWithTag("whatToDoText").GetComponent<Text>();
        tutorialText.text = tutorialMessages[progressCounter];
        whatToDoText.text = whatDoToMessages[progressCounter];
        gl = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gl.boss.GetComponent<Boss>().health <= 0 && !bossDead && trigger)
        {
            FunctionTimer.Create(delayKilledBoss, 1f);
            trigger = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            progressCounter++;
            paused = !paused;
            switch(progressCounter)
            {
                case 1:
                    paused = true;
                    tutorialText.text = tutorialMessages[progressCounter];
                    whatToDoText.text = whatDoToMessages[progressCounter];
                    break;

            }
            if(bossDead)
            {
                paused = false;
            }

        }

        if(paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1f;
            tutorialText.text = "";
            whatToDoText.text = "";
        }
    }

    public void KilledBoss()
    {
        paused = true;
        bossDead = true;
        tutorialText.text = "You've defeated the core! hopefully thats all there is to it.";
        whatToDoText.text = "Stand on the red circle to continue.";
    }
}
