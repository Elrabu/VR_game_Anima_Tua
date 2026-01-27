using UnityEngine;
using UnityEngine.UI;
public class SaveDataSceneCompletion : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        SaveData.Instance.settings.levels.Add("Dungeon01");
        SaveData.Instance.SaveToJson();
        Debug.Log("Dungeon saved!");
    }
}
