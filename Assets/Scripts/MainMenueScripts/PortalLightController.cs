using UnityEngine;

public class PortalLightController : MonoBehaviour
{
    [System.Serializable]
    public class PortalLight
    {
        public Light light;
        [HideInInspector] public float angleOffset;
    }

    [Header("Lichter")]
    public PortalLight[] lights;

    [Header("Orbit Einstellungen")]
    public float orbitRadius = 0.791f;       
    public float orbitSpeed = 100f;
    
    [Header("VFX Anpassung")]
    public float scaleX = 1.0f;              
    public float scaleY = 1.33f;             
    public Vector3 visualOffset = new Vector3(0f, 0.459f, 0f); 

    [Header("Intensität")]
    public float baseIntensity = 2.5f;

    void Start()
    {
        float angleStep = 360f / lights.Length;
        for (int i = 0; i < lights.Length; i++)
        {
            if (lights[i].light != null)
            {
                lights[i].angleOffset = i * angleStep;
                lights[i].light.renderMode = LightRenderMode.ForcePixel;

                lights[i].light.intensity = baseIntensity;
                lights[i].light.shadows = LightShadows.None;
            }
        }
    }

    void Update()
    {
        // Wir berechnen das Zentrum inkl. Offset im lokalen Raum
        Vector3 orbitCenter = transform.TransformPoint(visualOffset);

        for (int i = 0; i < lights.Length; i++)
        {
            PortalLight entry = lights[i];
            if (entry.light == null) continue;

            float currentAngle = (Time.time * orbitSpeed + entry.angleOffset) * Mathf.Deg2Rad;

            // Lokale X und Y Positionen berechnen
            float localX = Mathf.Cos(currentAngle) * orbitRadius * scaleX;
            float localY = Mathf.Sin(currentAngle) * orbitRadius * scaleY;

            // Wir nutzen transform.right und transform.up, um die Lichter 
            // IMMER parallel zur Fläche des Objekts auszurichten
            Vector3 worldPos = orbitCenter + (transform.right * localX) + (transform.up * localY);

            entry.light.transform.position = worldPos;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 orbitCenter = transform.TransformPoint(visualOffset);
        int segments = 64;
        
        Vector3 lastPoint = orbitCenter + (transform.right * orbitRadius * scaleX);
        
        for (int i = 1; i <= segments; i++)
        {
            float a = (float)i / segments * Mathf.PI * 2f;
            float x = Mathf.Cos(a) * orbitRadius * scaleX;
            float y = Mathf.Sin(a) * orbitRadius * scaleY;
            
            Vector3 nextPoint = orbitCenter + (transform.right * x) + (transform.up * y);
            Gizmos.DrawLine(lastPoint, nextPoint);
            lastPoint = nextPoint;
        }
    }
}