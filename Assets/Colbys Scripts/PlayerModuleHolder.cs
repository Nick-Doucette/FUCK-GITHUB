using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerModuleHolder : MonoBehaviour
{

    [SerializeField]
    private GameObject currentModule;

    [SerializeField]
    private List<GameObject> modules;

    public int activeModule = 0;
    private bool haveCheck = false;
    private string tempString;
    private GameObject weaponSprite;

    private Sprite gunSprite;
    private Sprite spreadSprite;
    private Sprite rocketSprite;
    private Sprite shieldSprite;

    // Start is called before the first frame update
    void Start()
    {
        modules = new List<GameObject>();
        modules.Add(currentModule);
        weaponSprite = GameObject.Find("WeaponSprite");
        gunSprite = Resources.Load<Sprite>("mod_blaster");
        rocketSprite = Resources.Load<Sprite>("mod_rpg");
        spreadSprite = Resources.Load<Sprite>("mod_spread");
        shieldSprite = Resources.Load<Sprite>("mod_shield");
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
            currentModule.gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>().enabled = false;       //disable sprite for current module
            currentModule.GetComponent<Module>().shooting = false;
            currentModule = modules[activeModule];                                                                  //set the new current module
            currentModule.gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>().enabled = true;        //enable sprite for the new current module

            switch(currentModule.name)
            {
                case "Gun Module":
                    weaponSprite.GetComponent<Image>().sprite = gunSprite;
                    break;

                case "Burst Module":
                    weaponSprite.GetComponent<Image>().sprite = spreadSprite;
                    break;

                case "Rocket Module":
                    weaponSprite.GetComponent<Image>().sprite = rocketSprite;
                    break;

                case "Shield Module":
                    weaponSprite.GetComponent<Image>().sprite = shieldSprite;
                    break;
            }
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
            tempString = collision.gameObject.name.Substring(0, collision.gameObject.name.Length - 7);
            for (int counter = 0; counter < modules.Count; counter++)
            {
                if (modules[counter].name == tempString)
                {
                    haveCheck = true;
                    counter = modules.Count + 1;
                }
            }

            if (!haveCheck)
            {
                modules.Add(gameObject.transform.Find(tempString).gameObject);
                Debug.Log("picked up " + tempString);
            }
            tempString = null;
            haveCheck = false;
        }
    }
    //(Clone)

}
