using UnityEngine;
using System.Collections.Generic;

public class SaveData : MonoBehaviour
{
    public Inventory inventory = new Inventory();

    private void Start()
    {
        //SaveToJson();
        //LoadFromJson();
    }

    public void SaveToJson()
    {
        string inventoryData = JsonUtility.ToJson(inventory);
        string filePath = Application.persistentDataPath + "/InventoryData.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, inventoryData);
        Debug.Log("data read!");
    }

    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/InventoryData.json";
        string inventoryData = System.IO.File.ReadAllText(filePath);

        inventory = JsonUtility.FromJson<Inventory>(inventoryData);
        Debug.Log("Data loaded");
    }
}

[System.Serializable]
public class Inventory
{
    public int goldCoins = 100;
    public bool isFull = true;
    public List<items> items = new List<items>();
}

[System.Serializable]
public class items
{
    public string name;
    public string desc;
} 
