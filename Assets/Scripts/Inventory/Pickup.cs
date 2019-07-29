using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : InventoryItem
{
    public Inventory iv;

    private bool canPickup;

    void Start()
    {
        iv = GameObject.FindObjectOfType<Inventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canPickup)
        {
            iv.inventoryItem.Add(this.gameObject);
            iv.UpdateInventory();
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && iv.inventoryItem.Count < 12)
        {
            canPickup = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            canPickup = false;
        }
    }

}