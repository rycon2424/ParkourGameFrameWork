using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public int itemSlot;

    private Inventory iv;

    void Start()
    {
        iv = GameObject.FindObjectOfType<Inventory>();
    }

    public void ShowStats()
    {
        iv.SelectedItem(itemSlot);
    }
}
