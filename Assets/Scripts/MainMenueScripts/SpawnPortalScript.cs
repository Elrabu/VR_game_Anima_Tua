using UnityEngine;

public class SpawnPortalScript : MonoBehaviour
{
    [SerializeField] private GameObject portal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void spawnPortal()
    {
        portal.SetActive(true);
    }
}
