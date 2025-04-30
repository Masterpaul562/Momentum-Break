using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePunchCombo : StateMachineBehaviour
{
    private GameObject Player;
    
    private void Awake() {
        Player = GameObject.FindWithTag("Player");
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isThirdAttack", false);
        animator.SetBool("UpperCut", false);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Player.GetComponent<Move_Player>().punched){
            animator.SetTrigger("Attack1");
           // Player.GetComponent<Move_Player>().isUpperCut = true;
            Player.GetComponent<Move_Player>().ResetPunch();
            Player.GetComponent<Move_Player>().punched = false;
            Player.GetComponent<Move_Player>().NormalPunch(0,15);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // Player.GetComponent<Move_Player>().isUpperCut = false;
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
