using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition1Behaviour : StateMachineBehaviour
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
        shouldStopAttacking = true;
        if(animator.GetBool("UpperCut")== true){
            animator.SetBool("UpperCut",false);
        }
       
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Player.GetComponent<Move_Player>().punched){
            float vert = Input.GetAxisRaw("Vertical");
            if(vert > 0.1){
            animator.SetTrigger("UpperCut");
            Player.GetComponent<Move_Player>().ResetPunch();
           // Player.GetComponent<Move_Player>().upperCutDamage = true;
            Player.GetComponent<Move_Player>().punched = false;
            shouldStopAttacking=false;
            Player.GetComponent<Move_Player>().NormalPunch(5,60);
            }else {
            animator.SetTrigger("Attack2");
            Player.GetComponent<Move_Player>().ResetPunch();
            Player.GetComponent<Move_Player>().punched = false;
            shouldStopAttacking=false;
            Player.GetComponent<Move_Player>().NormalPunch(0,15);
             
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (shouldStopAttacking)
        {
            Player.GetComponent<Move_Player>().isAttacking = false;
            animator.SetBool("isThirdAttack", false);
        } else
        {
            animator.SetBool("isThirdAttack", true);
        }
       // Player.GetComponent<Move_Player>().canUpperCut = false;
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
