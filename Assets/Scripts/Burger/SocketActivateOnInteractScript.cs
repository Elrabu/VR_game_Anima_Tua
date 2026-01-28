using UnityEngine;

public class SocketActivateOnInteractScript : MonoBehaviour
{
    [SerializeField] private GameObject socket;
    public void ActivateSocketObject()
    {
        socket.SetActive(true);
    }
}
