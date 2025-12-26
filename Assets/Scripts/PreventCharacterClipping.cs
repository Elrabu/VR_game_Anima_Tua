using UnityEngine;

public class UpdateCharacter : MonoBehaviour
{
    public Transform cameraTransform;
    public float minHeight = 1.0f;
    public float maxHeight = 2.2f;
    public float maxLeanDistance = 0.1f;
    public float collisionCheckRadius = 0.15f;
    private Vector3 lastHitMoveDirection;
    private bool hadSideCollision;

    private CharacterController controller;
    private Vector3 lastCameraLocalPos;

    void Awake()
    {
        controller = GetComponent<CharacterController>();

        if (cameraTransform == null)
        {
            Camera cam = GetComponentInChildren<Camera>();
            if (cam != null) cameraTransform = cam.transform;
        }

        lastCameraLocalPos = cameraTransform.localPosition;
    }

    void Update()
    {
        UpdateCharacterController();
        ResolveHeadCollision();
    }

    void UpdateCharacterController()
    {
        float height = Mathf.Clamp(cameraTransform.localPosition.y, minHeight, maxHeight);
        controller.height = height;

        Vector3 center = Vector3.zero;
        center.y = height / 2f;
        controller.center = center;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
    // Only care about side collisions
    if (Mathf.Abs(hit.normal.y) > 0.2f)
    {
        return;
    }

    lastHitMoveDirection = hit.moveDirection;
    hadSideCollision = true;
    }

    void ResolveHeadCollision()
{
    Vector3 currentLocalPos = cameraTransform.localPosition;
    Vector3 delta = currentLocalPos - lastCameraLocalPos;

    Vector3 horizontalDelta = new Vector3(delta.x, 0f, delta.z);

    if (horizontalDelta.sqrMagnitude < 0.00001f)
    {
        lastCameraLocalPos = currentLocalPos;
        return;
    }

    Vector3 worldMove = transform.TransformVector(horizontalDelta);
    worldMove.y = 0f; // HARD LOCK Y

    hadSideCollision = false;

    // Attempt move
    controller.Move(worldMove);

    // If collision occurred, push back using hit.moveDirection
    if (hadSideCollision)
    {
        Vector3 pushBack = -lastHitMoveDirection;
        pushBack.y = 0f;

        if (pushBack.sqrMagnitude > 0.00001f)
        {
            controller.Move(pushBack.normalized * worldMove.magnitude);
        }
    }

    lastCameraLocalPos = cameraTransform.localPosition;
}

}
