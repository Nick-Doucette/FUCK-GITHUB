using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.MonoBehaviours;
using CodeMonkey.Utils;
using System;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    Action endDeathAction;
    Animator anim;
    Player_Control pc;

    private void Start()
    {
        pc = GetComponent<Player_Control>();
        endDeathAction = endDeath;
        anim = GetComponent<Animator>();
    }
    public void startDeath()
    {
        anim.SetBool("playerDead", true);

        pc.playerCanMove = false;

        FunctionTimer.Create(endDeathAction, 3f);
    }

    private void endDeath()
    {
        SceneManager.LoadScene("Credits");
    }

    public void gotHit()
    {

    }
}
