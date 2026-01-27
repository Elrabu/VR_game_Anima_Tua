using UnityEngine;
using System.Collections;


public class CookScript : MonoBehaviour
{
    [SerializeField] private GameObject smokePrefab;
    [SerializeField] private AudioSource sizzlingSource;
    private GameObject smoke;
    private bool cooking = false;
   
    //private GameObject patty;
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "patty" || collision.gameObject.name == "patty(Clone)")
        {
            //Debug.Log("Interacted with: " + collision.gameObject.name);
            smoke = Instantiate(smokePrefab, collision.gameObject.transform.position, smokePrefab.transform.rotation);

            var follow = smoke.GetComponent<GameObjectFollowScript>();
            follow.hand = collision.gameObject;
            sizzlingSource.Play();
            cooking = true;
            StartCoroutine(Cook(collision.gameObject));
        }
    }

    void OnTriggerExit(Collider collision)
    {
       
        if (collision.gameObject.name == "patty" || collision.gameObject.name == "patty(Clone)")
        {   
            FadeOutAndStop(6f);
            cooking = false;
            Destroy(smoke);
        }
    }

    IEnumerator Cook(GameObject patty)
    {
        // Wait 2 seconds
        yield return new WaitForSeconds(2.0f);

        if (patty != null)
        {
            Transform grilled = patty.transform.Find("patty_grilled");
            Transform raw = patty.transform.Find("patty_raw");
            Debug.Log(cooking);

            if (grilled != null && raw != null && cooking)
            {
                grilled.gameObject.SetActive(true);
                raw.gameObject.SetActive(false);
            }
        }
    }
    public void FadeOutAndStop(float fadeDuration)
    {
        StartCoroutine(FadeOutCoroutine(fadeDuration));
    }

    private IEnumerator FadeOutCoroutine(float duration)
    {
        float startVolume = sizzlingSource.volume;

        while (sizzlingSource.volume > 0f)
        {
            sizzlingSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        sizzlingSource.Stop();
        sizzlingSource.volume = startVolume; // Lautstärke zurücksetzen
    }
}
