using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt_Script : MonoBehaviour
{
    // Grunt Script handles any other grunt classes that will fight the player, the behaviour of each grunt is determined by another script

    // Start is called before the first frame update


    [SerializeField] private float maxHealth;
    [SerializeField] private float health;

    [SerializeField] private GameObject HealthBar;

    private float waterDamageModifier = 1f;
    private float iceDamageModifier = 1f;
    private float fireDamageModifier = 1f;
    void Start()
    {
        HealthBar.GetComponent<HealthBar_Script>().UpdateHealthBar(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float amountOfDamage, int typeOfDamage)
    {


        switch (typeOfDamage)
        {
            case 0:
                Debug.Log("Taking Fire Damage");
                amountOfDamage = amountOfDamage * fireDamageModifier;
                health -= amountOfDamage;
                break;

            case 1:
                Debug.Log("Taking Water Damage");
                amountOfDamage = amountOfDamage * waterDamageModifier;
                health -= amountOfDamage;
                Debug.Log(health);
                break;

            case 2:
                Debug.Log("Taking Ice Damage");
                amountOfDamage = amountOfDamage * iceDamageModifier;
                health -= amountOfDamage;
                break;
        }
        HealthBar.GetComponent<HealthBar_Script>().UpdateHealthBar(health, maxHealth);

    }
}
