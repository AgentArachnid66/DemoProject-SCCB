using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct FInventorySlot
{
    private Object Object;
    private int Quantity;

    public FInventorySlot(Object InObject, int InQuantity) : this()
    {
        Object = InObject;
        Quantity = InQuantity;
    }


    public bool IsSlotOpen()
    {
        return Quantity <= 0 && ReferenceEquals(Object, null); 
    }

    public int GetItemQuantity(Object ObjectToCheck)
    {
        return ReferenceEquals(ObjectToCheck, Object) ? Quantity : 0;
    }

    public Object GetObject()
    {
        return Object;
    }

}



public abstract class InventoryContainer
{

    protected List<FInventorySlot> InventorySlots = new List<FInventorySlot>();

    public virtual bool AddInventoryItem(IItemUsageInterface item, int SlotIndex)
    {


        return true;
    }

    public int GetFreeSlot()
    {
        // Iterate through the slots, find the first one that is open
        for(int i = 0; i < InventorySlots.Count; i++)
        {
            if (InventorySlots[i].IsSlotOpen())
            {
                return i;
            }
        }

        return -1;
    }

    

}
