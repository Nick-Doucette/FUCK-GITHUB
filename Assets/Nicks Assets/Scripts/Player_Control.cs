using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Camera cam;
    private Vector2 playerMousePosition;
    private Vector2 newPosition;
    private Vector2 lookDirection;
    private Vector3 moveDirection;
    private Vector2 movementInput;
    private Transform playerTrans;
    private bool shooting = false;
    private float moveX = 0f;
    private float moveY = 0f;
    private float angle = 0f;
    private const float MOVE_SPEED = 6f;

    // Start is called before the first frame update
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerTrans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //---------------------Get Player Input-----------------------------
        shooting = Input.GetMouseButton(0);
        playerMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        //------------------------------------------------------------------

        moveX = 0f;
        moveY = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveY = 1;

        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1;

        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1;

        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = 1;

        }

        //------------------------------------------------------Shooting code--------------------------------------------------------
        if (shooting)
        {
            //PEW PEW
        }
        //---------------------------------------------------------------------------------------------------------------------------

        moveDirection = new Vector3(moveX, moveY).normalized;
    }

    private void FixedUpdate()
    {
        //-----------------------------------------Player looking-----------------------------------------------------
        newPosition = new Vector2(playerTrans.transform.position.x, playerTrans.transform.position.y);

        lookDirection = playerMousePosition - newPosition;

        lookDirection = lookDirection.normalized;

        angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        rigidbody2D.rotation = angle;
        //------------------------------------------------------------------------------------------------------------

        rigidbody2D.velocity = moveDirection * MOVE_SPEED;
    }
}
