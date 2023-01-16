using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Crab : MonoBehaviour
{
    int health = 2000;
    float stamina = 100;
    float distance;
    //RUSH SİSTEMİNE BAKILACAK.
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;
    [SerializeField] Animator animator;
    string[] attackTypes = {"Attack_1","Attack_2","Attack_3","Attack_4","Attack_5"};
    bool isMove,isAttack;
    float isAttackTime,isMoveTime;
    void Start()
    {
        InvokeRepeating(nameof(MoveTime),0,1);
    }

    
    void Update()
    {
        distance = Mathf.Abs(Vector3.Distance(player.transform.position, transform.position));

        if(distance >=30){
            
            Rush();
        }
        else if(distance >= 8.5f){
            Move();
            isMove = true;
            isAttack = false;
        }
        else if(distance <= 8){
            Attack();
            isAttack = true;
            isMove = false;
        }
    }
    void Rush(){
        agent.SetDestination(player.position);
    }
    void Move(){
        if(isMove){
            agent.SetDestination(player.position);
            MoveAnim();
        }
    }
    void MoveAnim(){
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Armature|Attack_2") && isMoveTime ==1){
            animator.SetTrigger("Walk_Cycle_2");    
            isMoveTime = 0;
        }
    }
    void MoveTime(){
        if(distance >= 30f){
            agent.speed = 15;
            animator.SetTrigger("Walk_Cycle_1");
        }
            
        else{
            agent.speed = 3.5f;
            isMoveTime = 1;
        }    
    }
    void Attack(){
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime*3);
        if(isAttackTime ==0 && isAttack){
            agent.isStopped = false;
            StartCoroutine(nameof(AttackTime));
        }
    }
    IEnumerator AttackTime()
    {
        isAttackTime++;
        int random = Random.Range(0,5);
        animator.SetTrigger(attackTypes[random]);
        yield return new WaitForSeconds(1.5f);
        isAttackTime = 0;
    }
}
