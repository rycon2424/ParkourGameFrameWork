using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryCanvas;
    public List<GameObject> inventoryItem = new List<GameObject>();
    public Text[] inventoryText;
    float oldTimeScale;

    private PlayerInput pi;
    private bool canvasOpen;
    private PlayerController playerRef;

    private void Start()
    {
        pi = GameObject.FindObjectOfType<PlayerInput>();
        playerRef = pi.gameObject.GetComponent<PlayerController>();
        UpdateInventory();
        useAbleButton.interactable = false;
        removeButton.interactable = false;
        oldTimeScale = Time.timeScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(pi.inventory) && GAME_OVER.gameOver == false)
        {
            canvasOpen = !canvasOpen;
            ShowHideInventory();
        }

        //buttons[selectedItem].GetComponent<Image>().color = Color.blue;
    }

    public void HideInventory()
    {
        inventoryCanvas.SetActive(false);
        Time.timeScale = oldTimeScale;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void ShowHideInventory()
    {
        if (canvasOpen)
        {
            inventoryCanvas.SetActive(true);
            UpdateInventory();
            UpdateInfo();
            Time.timeScale = 0.1f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (!canvasOpen)
        {
            inventoryCanvas.SetActive(false);
            Time.timeScale = oldTimeScale;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public Button[] buttons;

    public void UpdateInventory()
    {
        for (int i = 0; i < inventoryText.Length; i++)
        {
            if (i >= inventoryItem.Count)
            {
                inventoryText[i].text = "";
                buttons[i].interactable = false;
            }
            else
            {
                inventoryText[i].text = inventoryItem[i].GetComponent<InventoryItem>().nameObject;
                buttons[i].interactable = true;
            }
        }
    }

    public void RemoveItem()
    {
        inventoryItem.Remove(inventoryItem[selectedItem]);
        SelectedItem(selectedItem = selectedItem - 1);
    }

    public Text description;
    public Button useAbleButton;
    public Button removeButton;

    public int selectedItem;

    public void SelectedItem(int itemSlot)
    {
        if (itemSlot < 0)
        {
            itemSlot = 0;
        }
        selectedItem = itemSlot;
        UpdateInventory();
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        if (inventoryItem.Count < 1)
        {
            removeButton.interactable = false;
            useAbleButton.interactable = false;
            return;
        }
        description.text = inventoryItem[selectedItem].GetComponent<InventoryItem>().description;
        AnimatorStateInfo animState = playerRef.Anim.GetCurrentAnimatorStateInfo(0);
        if (inventoryItem[selectedItem].GetComponent<InventoryItem>().useAble == true && animState.IsName("Idle"))
        {
            useAbleButton.interactable = true;
        }
        else
        {
            useAbleButton.interactable = false;
        }
        if (inventoryItem[selectedItem].GetComponent<InventoryItem>().discardAble == true)
        {
            removeButton.interactable = true;
        }
        else
        {
            removeButton.interactable = false;
        }
    }
    
    public void UseItem()
    {
        switch (inventoryItem[selectedItem].GetComponent<InventoryItem>().type)
        {
            case InventoryItem.Type.torch:
                playerRef.StopMoving();
                playerRef.StateMachine.GoToState<Torch>();
                break;

            case InventoryItem.Type.healthkit:
                break;

            case InventoryItem.Type.NAN:
                break;

            default:
                break;
        }
        if (inventoryItem[selectedItem].GetComponent<InventoryItem>().destroyOnUse == true)
        {
            inventoryItem.Remove(inventoryItem[selectedItem]);
        }
        SelectedItem(selectedItem = selectedItem - 1);
    }

    public GameObject doorInfo;
    public Text infoDoorText;

    public void Opendoor(bool hasRightKey, string itemName)
    {
        Debug.Log(hasRightKey + " " + itemName);
        doorInfo.SetActive(true);
        if (hasRightKey == true)
        {
            infoDoorText.text = "Used " + itemName + " to unlock the door";
        }
        else if (hasRightKey == false)
        {
            infoDoorText.text = "It's Locked";
        }
        Invoke("HideDoorInfo", 2);
    }

    void HideDoorInfo()
    {
        doorInfo.SetActive(false);
    }

}
