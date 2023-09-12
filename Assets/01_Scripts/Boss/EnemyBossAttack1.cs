using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossAttack1 : StateMachineBehaviour
{
    Boss enemyBoss;
    float bulletCount;
    float timer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemyBoss == null) enemyBoss = animator.GetComponent<Boss>();
        bulletCount = Random.Range(enemyBoss.minBulletCount, enemyBoss.maxBulletCount);
        timer = 0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(timer < enemyBoss.timeBtwShoot / 2f)
        {
            timer += Time.deltaTime;
        }
        else
        {
            enemyBoss.ShootWhitRandAngle();
            bulletCount--;
            timer = 0f;
        }

        if(bulletCount <= 0)
        {
            Debug.Log("bulletenelse " + bulletCount);
            animator.SetTrigger("ChangeState");
        }
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
