using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Control : MonoBehaviour
{

    private Transform playerPos;
    private GameObject mainCam;

    private void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("ourPlayer").transform;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        mainCam.transform.position = new Vector3(playerPos.position.x, playerPos.position.y, mainCam.transform.position.z);
    }
}
