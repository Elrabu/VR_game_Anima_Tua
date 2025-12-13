using UnityEngine;

public class CharacterControllerFollowScript : MonoBehaviour
{
    [SerializeField] private Camera cam;

    // Update is called once per frame
    void Update()
    {
        Vector3 worldOffset = new Vector3(0f, -0.5f, 0f);
        //Debug.Log("Position: " + cam.transform.position);
        transform.position = cam.transform.position + worldOffset;

        Vector3 forward = cam.transform.forward; //world forward position

        forward.y = 0f; //remove pitch and roll influence

        if (forward.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(forward);
        }
    }
}
