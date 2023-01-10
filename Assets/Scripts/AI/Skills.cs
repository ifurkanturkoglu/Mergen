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
    float fireballCooldown,flameCooldown = 0;
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
        yield return new WaitForSeconds(fireballCooldown);
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
        yield return new WaitForSeconds(flameCooldown);
        flameCooldown = 0;
        
    }
}
