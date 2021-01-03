using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.MonoBehaviours;
using CodeMonkey.Utils;
using System;

public class DisplayExplosion : MonoBehaviour
{
    public Action DisplayAction;
    public SpriteRenderer explosionSprite;

    private void Start()
    {
        DisplayAction = exposionOn;

    }

    public void ShowExplosion()
    {
        FunctionTimer.Create(DisplayAction, 1f);
        //DisplayAction = explosionOff;
       // explosionOff();
    }

    public void exposionOn()
    {
        explosionSprite.enabled = true;
    }

    private void explosionOff()
    {
        explosionSprite.enabled = false;
    }
}
