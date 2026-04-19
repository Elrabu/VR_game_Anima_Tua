using UnityEngine;

public class TeleportIndicatorPulse : MonoBehaviour
{
    [Header("Scale Pulse")]
    [SerializeField] private bool enableScalePulse = true;
    [SerializeField] private float pulseSpeed = 3.5f;
    [SerializeField] private float pulseAmount = 0.12f;

    [Header("Hover Motion")]
    [SerializeField] private bool enableHover = true;
    [SerializeField] private float hoverSpeed = 2.5f;
    [SerializeField] private float hoverAmount = 0.01f;

    [Header("Rotation")]
    [SerializeField] private bool enableRotation = true;
    [SerializeField] private float rotationSpeed = 80f;

    [Header("Emission")]
    [SerializeField] private bool enableEmissionPulse = true;
    [SerializeField] private Color emissionColor = new Color(0.2f, 1f, 0.95f);
    [SerializeField] private float minEmission = 0.2f;
    [SerializeField] private float maxEmission = 1.5f;

    private Vector3 baseLocalPosition;
    private Vector3 baseLocalScale;
    private Renderer[] renderers;

    public void Configure(
        float newPulseSpeed,
        float newPulseAmount,
        float newHoverSpeed,
        float newHoverAmount,
        float newRotationSpeed,
        Color newEmissionColor,
        float newMinEmission,
        float newMaxEmission)
    {
        pulseSpeed = newPulseSpeed;
        pulseAmount = newPulseAmount;
        hoverSpeed = newHoverSpeed;
        hoverAmount = newHoverAmount;
        rotationSpeed = newRotationSpeed;
        emissionColor = newEmissionColor;
        minEmission = newMinEmission;
        maxEmission = newMaxEmission;
    }

    private void Awake()
    {
        CaptureBaseTransform();
        CacheRenderers();
    }

    private void OnEnable()
    {
        CaptureBaseTransform();
        CacheRenderers();
    }

    private void Update()
    {
        float pulse = 0.5f + 0.5f * Mathf.Sin(Time.time * pulseSpeed * Mathf.PI * 2f);

        if (enableScalePulse)
        {
            float scaleFactor = 1f + ((pulse * 2f - 1f) * pulseAmount);
            transform.localScale = baseLocalScale * scaleFactor;
        }

        if (enableHover)
        {
            float hover = Mathf.Sin(Time.time * hoverSpeed * Mathf.PI * 2f) * hoverAmount;
            transform.localPosition = baseLocalPosition + new Vector3(0f, hover, 0f);
        }

        if (enableRotation)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
        }

        if (enableEmissionPulse)
        {
            ApplyEmission(pulse);
        }
    }

    private void CaptureBaseTransform()
    {
        baseLocalPosition = transform.localPosition;
        baseLocalScale = transform.localScale;
    }

    private void CacheRenderers()
    {
        renderers = GetComponentsInChildren<Renderer>(true);
    }

    private void ApplyEmission(float pulse)
    {
        if (renderers == null || renderers.Length == 0)
        {
            return;
        }

        float intensity = Mathf.Lerp(minEmission, maxEmission, pulse);
        Color finalEmission = emissionColor * intensity;

        for (int i = 0; i < renderers.Length; i++)
        {
            Material[] materials = renderers[i].materials;
            for (int j = 0; j < materials.Length; j++)
            {
                Material mat = materials[j];
                if (mat == null || !mat.HasProperty("_EmissionColor"))
                {
                    continue;
                }

                mat.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", finalEmission);
            }
        }
    }
}
