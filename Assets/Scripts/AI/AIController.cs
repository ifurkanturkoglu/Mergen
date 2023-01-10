using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    
    [SerializeField] protected Transform target;
    [SerializeField] protected float distance;
    
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
        if(distance > 15 &&distance <= 20){
            Skills.Instance.Fireball(true);
        }
        else if(distance <=15){
            Skills.Instance.Flame(false);
        }
        
        
    }
}
