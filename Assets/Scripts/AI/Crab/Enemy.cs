using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    //Health ve stamina abstract olayına bakılacka.
    public abstract void Move();
    public abstract void TakeDamage();
    protected int health;
    float stamina ;
   
}
