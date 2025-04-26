using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition2Behaviour : StateMachineBehaviour
{
    private GameObject Player;
    private bool shouldStopAttacking;

    private void Awake() {
        Player = GameObject.FindWithTag("Player");
    }
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        Player.GetComponent<Move_Player>().canPunch = true;
        // Player.GetComponent<Move_Player>().isAttacking = true;
        shouldStopAttacking = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Player.GetComponent<Move_Player>().punched){
            animator.SetTrigger("Attack3");
            Player.GetComponent<Move_Player>().ResetPunch();
            Player.GetComponent<Move_Player>().punched = false;
            shouldStopAttacking = false;
            
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


        if (shouldStopAttacking)
        {
            Player.GetComponent<Move_Player>().isAttacking = false;
          
        }

    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
