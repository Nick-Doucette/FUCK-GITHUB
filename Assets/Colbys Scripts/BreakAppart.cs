using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakAppart : MonoBehaviour
{

    public GameObject destructableBoss;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ExplodeThisGameObject()
    {
        GameObject destructable = (GameObject)Instantiate(destructableBoss);

        destructable.transform.position = transform.position;
    }
}
