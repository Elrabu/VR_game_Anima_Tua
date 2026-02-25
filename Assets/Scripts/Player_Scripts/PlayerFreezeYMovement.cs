using UnityEngine;

public class PlayerFreezeYMovement : MonoBehaviour
{
    private float lockedY;

    void Start()
    {
        lockedY = transform.position.y;
    }

    void LateUpdate()
    {
        // Force player to stay on one plane
        Vector3 pos = transform.position;
        pos.y = lockedY;
        transform.position = pos;
    }
}
