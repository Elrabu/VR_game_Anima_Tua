using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance;

    [SerializeField] Image fadeImage;
    [SerializeField] float duration = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator Fade(float start, float end)
    {
        float t = 0;
        Color c = fadeImage.color;

        while (t < duration)
        {
            float a = Mathf.Lerp(start, end, t / duration);
            c.a = a;
            fadeImage.color = c;

            t += Time.deltaTime;
            yield return null;
        }

        c.a = end;
        fadeImage.color = c;
    }
}