using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScripts : MonoBehaviour
{

    public void PlayGameChangeScene()
    {
        SceneManager.LoadScene("Colbys Scene");
    }
    public void CreditsChangeScene()
    {
        SceneManager.LoadScene("Credits");
    }

    public void MainMenuChangeScene()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
