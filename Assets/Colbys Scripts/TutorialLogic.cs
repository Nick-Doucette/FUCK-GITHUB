using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialLogic : MonoBehaviour
{

    private bool paused = true;
    public int progressCounter = 0;
    public string[] tutorialMessages;
    public string[] whatDoToMessages;
    private Text tutorialText;
    private Text whatToDoText;

    // Start is called before the first frame update
    void Start()
    {
       
        tutorialText = GameObject.FindGameObjectWithTag("TutorialText").GetComponent<Text>();
        whatToDoText = GameObject.FindGameObjectWithTag("whatToDoText").GetComponent<Text>();
        tutorialText.text = tutorialMessages[progressCounter];
        whatToDoText.text = whatDoToMessages[progressCounter];
    }

    // Update is called once per frame
    void Update()
    {
        
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

                case 2:
                    
                    break;
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
}
