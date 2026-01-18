using UnityEngine;
using System.Collections;

public class CookScript : MonoBehaviour
{
    [SerializeField] private GameObject smokePrefab;
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

            cooking = true;
            StartCoroutine(Cook(collision.gameObject));
        }
    }

    void OnTriggerExit(Collider collision)
    {
        cooking = false;
        if (collision.gameObject.name == "patty" || collision.gameObject.name == "patty(Clone)")
        {
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

            if (grilled != null && raw != null && cooking)
            {
                grilled.gameObject.SetActive(true);
                raw.gameObject.SetActive(false);
            }
        }
    }
}
