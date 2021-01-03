﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModuleHolder : MonoBehaviour
{

    [SerializeField]
    private GameObject currentModule;

    [SerializeField]
    private List<GameObject> modules;

    public int activeModule = 0;
    private bool haveCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        modules = new List<GameObject>();
        modules.Add(currentModule);
    }

    // Update is called once per frame
    void Update()
    {

        //cycles active module with scroll wheel
        if (Input.mouseScrollDelta.y > 0f) { activeModule++; }
        else if (Input.mouseScrollDelta.y < 0f) { activeModule--; }
        
        //keeps active module in valid range
        if (activeModule > modules.Count - 1 ) { activeModule = 0; }
        else if (activeModule < 0) { activeModule = modules.Count - 1; }

        //check if we are switching modules
        if (currentModule != modules[activeModule])
        {
            currentModule.GetComponent<SpriteRenderer>().enabled = false;       //disable sprite for current module
            currentModule = modules[activeModule];                              //set the new current module
            currentModule.GetComponent<SpriteRenderer>().enabled = true;        //enable sprite for the new current module
        }

        if (Input.GetMouseButtonDown(0) && currentModule != null)
        {
            currentModule.GetComponent<Module>().isClicking = true;
        }
        else if(Input.GetMouseButtonUp(0) && currentModule != null)
        {
            currentModule.GetComponent<Module>().isClicking = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player Module" && !collision.gameObject.GetComponent<Module>().isAttachedToBoss)
        {
            for (int counter = 0; counter < modules.Count; counter++)
            {
                if (modules[counter].name == collision.gameObject.name)
                {
                    haveCheck = true;
                    counter = modules.Count + 1;
                }
            }

            if (!haveCheck)
            { 
                modules.Add(gameObject.transform.Find(collision.gameObject.name).gameObject);
                Debug.Log("picked up " + collision.gameObject.name);
            }
            haveCheck = false;
        }
    }


}
