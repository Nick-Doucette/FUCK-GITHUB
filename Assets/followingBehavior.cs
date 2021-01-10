using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followingBehavior : StateMachineBehaviour
{
    [SerializeField]
    private float maxMoveSpeed = 9f;
    [SerializeField]
    private float defaultMoveSpeed = 4f;
    [SerializeField]
    private float slowestMoveSpeed = 1f;

    [SerializeField]
    private float currentMoveSpeed = 0f;

    [SerializeField]
    private float distanceFromPlayer = 100;

    private Transform playerTransform;
    private Rigidbody2D rb;
    private Vector2 directionToMove;
    private string debugDistance;
    Boss boss;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        distanceFromPlayer = Vector3.Distance(playerTransform.position, animator.transform.position);

        boss = animator.GetComponent<Boss>();
        boss.bossAwake = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if(distanceFromPlayer < 4f)
        {
            debugDistance = "distance less than 4";

            currentMoveSpeed = slowestMoveSpeed;
        }
        else if(distanceFromPlayer > 4f && distanceFromPlayer < 8f)
        {
            debugDistance = "distance greater than 4, less than 8";
            currentMoveSpeed = defaultMoveSpeed;
        }else if (distanceFromPlayer > 8f)
        {
            debugDistance = "distance greater than 8 ";
            currentMoveSpeed = maxMoveSpeed;
        }

        directionToMove = Vector2.MoveTowards(animator.transform.position, playerTransform.position, currentMoveSpeed * Time.deltaTime);
        distanceFromPlayer = Vector3.Distance(playerTransform.position, animator.transform.position);
        rb.MovePosition(directionToMove);


        //Debug.Log(debugDistance);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
