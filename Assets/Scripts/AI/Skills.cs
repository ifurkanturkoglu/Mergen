using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : AIController
{
    public static Skills Instance;
    [SerializeField] Transform attackPosition;
    [SerializeField] GameObject fireball;
    [SerializeField] ParticleSystem flame;
    bool attackIsEnd = true;
    bool isJump;
    float fireballCooldown,flameCooldown,jumpCooldown = 0;
    void Start()
    {
        Instance = this;
    }
    public void Flame(bool isRange){
        if( flameCooldown ==0 && !isRange && attackIsEnd){
            StartCoroutine(nameof(FlameFireTime));
        }
    }

    public void Fireball(bool isRange){
        if(fireballCooldown ==0 && isRange && attackIsEnd){
            StartCoroutine(nameof(FireballFireTime));
        }
    }
    IEnumerator FireballFireTime()
    {
        attackIsEnd = false;
        while (fireballCooldown < 3)
        {
            fireballCooldown++;
            Instantiate(fireball, attackPosition.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
        attackIsEnd = true;
        yield return new WaitForSeconds(fireballCooldown/2);
        fireballCooldown = 0;
        
    }

    IEnumerator FlameFireTime(){
        attackIsEnd = false;
        flame.Play();
        while (flameCooldown <5)
        {
            flameCooldown++;
            yield return new WaitForSeconds(1f);
        }
        attackIsEnd = true;
        yield return new WaitForSeconds(flameCooldown/2);
        flameCooldown = 0;
        
    }

    public void Jump(){
        if(jumpCooldown ==0){
            StartCoroutine(nameof(JumpTimer));
        }
            
    }
    IEnumerator JumpTimer(){
        jumpCooldown++;
        animator.SetBool("isJump",true);
        yield return new WaitForSeconds(2f);
        animator.SetBool("isJump",false);
        target.GetComponent<Rigidbody>().AddForce(Vector3.up*300);
        while (jumpCooldown <9)
        {
            jumpCooldown++;
            yield return new WaitForSeconds(1f);
        }
        jumpCooldown = 0;
    }

}
