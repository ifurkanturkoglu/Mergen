using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Inventory",menuName ="Scriptable/Inventory")]
public class SCInvantory : ScriptableObject
{
    public List<Slot> inventorySlots = new List<Slot>();
    public void AddItem(SCItem item)
    {
        foreach (Slot slot in inventorySlots)
        {
            if (slot.item==item)
            {
                if (slot.item.canStackable)
                {
                    slot.itemCount++;
                }
            }
            else if(slot.isFull)
            {
                slot.AddItemToSlot(item);
                break;
            }
        }
    }
}
[System.Serializable]
public class Slot
{
    public bool isFull;
    public int itemCount;
    public SCItem item;
    public void AddItemToSlot(SCItem item)
    {
        this.item = item;
        if (item.canStackable ==false)
        {
            isFull = true;
        }
        itemCount++;
    }
}
