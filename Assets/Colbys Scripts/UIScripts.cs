using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScripts : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    private Vector2 mousePos;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    private void Start()
    {
        
    }

    private void Update()
    {
        mousePos.x = Mathf.Clamp(mousePos.x, minX, maxX);
        mousePos.y = Mathf.Clamp(mousePos.y, minY, maxY);
    }
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

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
