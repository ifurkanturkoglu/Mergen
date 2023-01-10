using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    Transform attackPos;
    void Start()
    {
        attackPos = GameObject.Find("AttackPosition").transform;
    }
    void Update()
    {
        transform.position += attackPos.forward*Time.deltaTime*25;
    }
    void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
