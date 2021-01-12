using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Control : MonoBehaviour
{

    private List<GameObject> panelList;

    private Vector2 startPos1;
    private Vector2 startPos2;
    private float endPos1;
    private float endPos2;
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool horizontal = true;
    private bool open = false;

    private void Awake()
    {
        panelList = new List<GameObject>();
        panelList.Add(gameObject.transform.Find("DoorPanel").gameObject);
        panelList.Add(gameObject.transform.Find("DoorPanel2").gameObject);
        startPos1 = new Vector2(panelList[0].transform.position.x, panelList[0].transform.position.y);
        startPos2 = new Vector2(panelList[1].transform.position.x, panelList[1].transform.position.y);
    }

    void Update()
    {
        if (horizontal)
        {
            endPos1 = startPos1.x - 0.5f;
            endPos2 = startPos2.x + 0.5f;
            if (panelList[0].transform.position.x < endPos1 || panelList[1].transform.position.x > endPos2)
            {
                panelList[0].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                panelList[1].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                open = true;
            }
        }
        else
        {
            endPos1 = startPos1.y - 0.5f;
            endPos2 = startPos2.y + 0.5f;
            if (panelList[0].transform.position.y < endPos1 || panelList[1].transform.position.y > endPos2)
            {
                panelList[0].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                panelList[1].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                open = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ourPlayer")
        {
            if (!open)
            {
                if (horizontal)
                {
                    panelList[0].GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed, 0));
                    panelList[1].GetComponent<Rigidbody2D>().AddForce(new Vector2(speed, 0));
                }
                else
                {
                    panelList[0].GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -speed));
                    panelList[1].GetComponent<Rigidbody2D>().AddForce(new Vector2(0, speed));
                }
            }
        }
    }
}
