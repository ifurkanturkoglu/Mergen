using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Crab")){
            Crab.Instance.TakeDamage();
        }
    }
}
