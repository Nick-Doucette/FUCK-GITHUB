using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;


public class TestingDamagePopups : MonoBehaviour
{
    [SerializeField]
    private Transform pfdamagePopup;

    // Start is called before the first frame update
    void Start()
    {
        //DamagePopup.Create(Vector3.zero, 300f);

    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //DamagePopup.Create(UtilsClass.GetMouseWorldPosition(),150f);

        }
    }

}
