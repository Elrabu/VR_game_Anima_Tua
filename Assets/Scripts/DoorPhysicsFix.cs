using UnityEngine;

public class DoorPhysicsFix : MonoBehaviour
{
    Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnExitSocket()
    {
        rb.isKinematic = false;
        rb.angularVelocity = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
        rb.WakeUp();
    }
}
