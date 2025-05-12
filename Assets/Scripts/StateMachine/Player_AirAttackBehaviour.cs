using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AirAttackBehaviour : StateMachineBehaviour
{
    private GameObject Player;
    
    private void Awake() {
        Player = GameObject.FindWithTag("Player");
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.GetComponent<Move_Player>().UpAirCollider.enabled = false;
        animator.SetBool("InAirAttack",false); 
        animator.SetBool("IsJumping",false); 
        //animator.SetBool("DoubleJump",false); 
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Player.GetComponent<Move_Player>().airPunched){
            float vert = Input.GetAxisRaw("Vertical");
            
           Player.GetComponent<Move_Player>().airPunched=false;
           animator.SetTrigger("AirAttack");
           animator.SetBool("InAirAttack",true);
           if(vert>0.01f){
                // Player.GetComponent<Move_Player>().NormalPunch(5,40);
                Player.GetComponent<Move_Player>().UpAirCollider.enabled = true;
            } else {
                 //Player.GetComponent<Move_Player>().NormalPunch(5,-100);
            }
           
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Player.GetComponent<Move_Player>().UpAirCollider.enabled = false;

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
