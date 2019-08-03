using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTon : MonoBehaviour
{
    private static SingleTon instance = null;
    public static SingleTon Instance
    {
        get { return instance; }
    }

    void Awake()
    {

        if (instance != null && instance != this)
        {

            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
        
    }

    public List<string> savedInventory = new List<string>();
    
    public void SaveInventory(string backup)
    {
        savedInventory.Add(backup);
    }

    public void ClearList()
    {
        savedInventory = new List<string>();
    }

}