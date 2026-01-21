using UnityEngine;

public class HeartbeatTest : MonoBehaviour
{
    public AudioSource tinitus;

    private static HeartbeatTest instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.Log("[HeartbeatTest] Duplicate instance, destroying " + gameObject.name);
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("[HeartbeatTest] Awake, id = " + GetInstanceID());
    }

    void OnDestroy()
    {
        Debug.Log("[HeartbeatTest] OnDestroy, id = " + GetInstanceID());
    }

    void Update()
    {
        // nur zum Testen:
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("[HeartbeatTest] PlayTinitus");
            tinitus.Play();
        }
    }
}