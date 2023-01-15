using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCItem : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private bool canStackable;
    [SerializeField] Sprite itemIcon;
    [SerializeField] GameObject itemPrefab;


}
