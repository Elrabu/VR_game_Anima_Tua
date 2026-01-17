using UnityEngine;
using System.Collections;

public class CookScript : MonoBehaviour
{
    [SerializeField] private GameObject smokePrefab;
    //private GameObject patty;
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "patty" || collision.gameObject.name == "patty(Clone)")
        {
            //Debug.Log("Interacted with: " + collision.gameObject.name);
            Instantiate(smokePrefab, collision.gameObject.transform.position, smokePrefab.transform.rotation);
            StartCoroutine(Cook(collision.gameObject));
        }
    }

    IEnumerator Cook(GameObject patty)
    {
        // Wait 2 seconds
        yield return new WaitForSeconds(2f);

        Transform grilled = patty.transform.Find("patty_grilled");
        Transform raw = patty.transform.Find("patty_raw");

        if (grilled != null && raw != null)
        {
            grilled.gameObject.SetActive(true);
            raw.gameObject.SetActive(false);
        }
    }
}
