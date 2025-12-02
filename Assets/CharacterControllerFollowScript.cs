using UnityEngine;

public class CharacterControllerFollowScript : MonoBehaviour
{
    public Camera cam;

    // Update is called once per frame
    void Update()
    {
        Vector3 worldOffset = new Vector3(0.2f, -0.5f, 0f);
        Debug.Log("Position: " + cam.transform.position);
        transform.position = cam.transform.position + worldOffset;
    }
}
