using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{

    private float MoveX;
    private float MoveY;
    [SerializeField]
    private float moveSpeed = 1;
    private Vector3 moveDirection;
    private Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveX = 0f;
        MoveY = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            MoveX -= 1;
        }
        if(Input.GetKey(KeyCode.D))
        {
            MoveX += 1;
        }
        if(Input.GetKey(KeyCode.S))
        {
            MoveY -= 1;
        }
        if(Input.GetKey(KeyCode.W))
        {
            MoveY += 1;
        }

        moveDirection = new Vector3(MoveX, MoveY).normalized;

        rb.velocity = moveDirection * moveSpeed;
        OrientPlayer();

    }

    void OrientPlayer()
    {
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        //Ta Daaa
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
