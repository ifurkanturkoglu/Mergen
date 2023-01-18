using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(BossAwake(other.gameObject));
    }
    IEnumerator BossAwake(GameObject boss){
        Crab.Instance.animator.SetTrigger("Intimidate_1");
        yield return new WaitForSeconds(4f);
        Crab.Instance.isAwake = true;
    }
}
