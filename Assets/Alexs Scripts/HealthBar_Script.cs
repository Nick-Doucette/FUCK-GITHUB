using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar_Script : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public Transform bar;
    [SerializeField] private float MaxHealth;
    [SerializeField] private float CurrentHealth;

    void Start()
    {
       

    }

    public void UpdateHealthBar(float currenthealth, float maxhealth)
    {
        CurrentHealth = currenthealth;
        MaxHealth = maxhealth;

        float scaledHealth = (CurrentHealth / MaxHealth);

        Debug.Log("health: " + scaledHealth);

        bar.localScale = new Vector3(scaledHealth, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateHealthBar();
    }
}
