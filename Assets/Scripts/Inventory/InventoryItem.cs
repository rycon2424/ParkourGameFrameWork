using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [Header("Name")]
    public string nameObject = "Unknown Item";

    [Header("IMPORTANT")]
    public int keyNumber = -1;

    [Header("Overall")]
    public int durability;
    public bool useAble;
    public bool destroyOnUse;
    public bool discardAble;

    [Header("ItemType")]
    public bool isTorch;

    [Header("Description")]
    public string description;
}