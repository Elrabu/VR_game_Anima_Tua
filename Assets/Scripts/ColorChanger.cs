using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Color hitColor = Color.red;

    public void ChangeColor()
    {
        Renderer r = GetComponent<Renderer>();
        if (r != null)
        {
            r.material.color = hitColor;
        }
    }
}
