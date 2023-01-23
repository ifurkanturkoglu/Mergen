using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public SCInvantory playerInventory;

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag=="Item")
        {
            
        }
    }
}
