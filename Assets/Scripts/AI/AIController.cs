using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIController : MonoBehaviour
{
    
    [SerializeField] protected Transform target;
    [SerializeField] protected float distance;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] protected Animator animator;
    
    bool playerIsRange;
    void Start()
    {     
        InvokeRepeating(nameof(AttackType),1,1);
    }
    void Update()
    {
        distance = Vector3.Distance(new Vector3(transform.position.x,0,transform.position.z),new Vector3(target.position.x,0,target.position.z));
        transform.LookAt(target);
    }

    
    void AttackType(){

        if(distance >= 10){
            // agent.isStopped = true;
        }
        if(distance > 15 && distance <= 20){
            Skills.Instance.Fireball(true);
        }
        if( distance > 10 &&distance <=15){
            Skills.Instance.Flame(false);
        }
        else if (distance <10){
            // agent.SetDestination(target.position);
            // agent.isStopped = false;
        }
        Skills.Instance.Jump();
    }
}
