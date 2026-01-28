using UnityEngine;

public class CharacterControllerFollowScript : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation;
    }
    void Update()
    {
        Vector3 worldOffset = new Vector3(0f, -0.6f, 0f);
        //Debug.Log("Position: " + cam.transform.position);
        transform.position = cam.transform.position + worldOffset;

        Vector3 forward = cam.transform.forward; //world forward position

        forward.y = 0f; //remove pitch and roll influence

        if (forward.sqrMagnitude > 0.01f)
        {
            // transform.rotation = Quaternion.LookRotation(forward);
            Quaternion targetRotation = Quaternion.LookRotation(forward) * initialRotation;
            transform.rotation = targetRotation;
        } 
    }
}
