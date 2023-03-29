using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Crab : Enemy
{
    
    public static Crab Instance;
    float distance;
    public bool isAwake;
    public override int health { get; set; }
    [SerializeField] WeaponController weaponController;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;
    [SerializeField] Material crabColor;
    public Animator animator;
    string[] attackTypes = { "Attack_1", "Attack_2", "Attack_3", "Attack_4", "Attack_5" };
    string[] takeDamageTypes = { "Take_Damage_1", "Take_Damage_2", "Take_Damage_3" };
    bool isMove, isAttack, isRush;
    float isAttackTime, isMoveTime, isTakeDamage;
    string attackingType;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        InvokeRepeating(nameof(MoveTime), 0, 1f);
        animator.SetTrigger("Sleep");
    }
    void Update()
    {
        distance = Mathf.Abs(Vector3.Distance(player.transform.position, transform.position));

        if (isAwake)
        {
            if (distance >= 30 || isRush)
            {
                Rush();
                isRush = true;
            }
            else if (distance >= 8.5f && !isRush)
            {
                Move();
                isMove = true;
                isAttack = false;
            }

            if (distance <= 8)
            {
                Attack();
                isAttack = true;
                isMove = false;
                isRush = false;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.name.Equals("Player"))
        {
            StartCoroutine(AddForcePlayer());
        }
    }
    
  


    void Rush()
    {
        agent.SetDestination(player.position);
    }
    public override void Move()
    {
        if (isMove)
        {
            agent.SetDestination(player.position);
            MoveAnim();
        }
    }
    void MoveAnim()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Armature|Attack_2") && isMoveTime == 1)
        {
            animator.SetTrigger("Walk_Cycle_2");
            isMoveTime = 0;
        }
    }
    void MoveTime()
    {
        if (isAwake)
        {
            if (distance >= 30f)
            {
                agent.speed = 15;
                animator.SetTrigger("Walk_Cycle_1");
            }

            else
            {
                agent.speed = 3.5f;
                isMoveTime = 1;
            }
        }

    }
    void Attack()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime/5);
        if (isAttackTime == 0 && isAttack)
        {
            agent.isStopped = false;
            StartCoroutine(nameof(AttackTime));
        }
    }
    IEnumerator AttackTime()
    {
        isAttackTime++;
        int random = Random.Range(0, attackTypes.Length);
        animator.SetTrigger(attackTypes[random]);
        attackingType = attackTypes[random];
        yield return new WaitForSeconds(3f);
        isAttackTime = 0;
    }

    IEnumerator TakeDamageTime()
    {
        isTakeDamage = 1;
        health -= 20;
        crabColor.color = Color.red;
        int random = Random.Range(0, takeDamageTypes.Length);
        animator.SetTrigger(takeDamageTypes[0]);
        yield return new WaitForSeconds(1);
        isTakeDamage= 0;
        crabColor.color = Color.white;
    }

    public override void TakeDamage()
    {
        if (isTakeDamage == 0 && weaponController.isAttack)
        {
            StartCoroutine(TakeDamageTime());
        }

    }
    IEnumerator AddForcePlayer()
    {
        float count = 0;
        Vector3 force;
        while (count < 1.6f && attackingType.Equals("Attack_1"))
        {
            count += Time.deltaTime;
            playerRb.AddForce(transform.forward * 55555f * Time.deltaTime, ForceMode.Impulse);
            force = count > .8f ? Vector3.up * -555f : Vector3.up * 555f;
            playerRb.AddForce(force * Time.deltaTime, ForceMode.Impulse);
            yield return null;
        }

    }
}
