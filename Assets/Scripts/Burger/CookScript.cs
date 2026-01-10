using UnityEngine;

public class CookScript : MonoBehaviour
{
    //private GameObject patty;
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "patty" || collision.gameObject.name == "patty(Clone)")
        {
        //    Debug.Log("Interacted with: " + collision.gameObject.name);
            
            Transform grilled = collision.transform.Find("patty_grilled");
            Transform raw = collision.transform.Find("patty_raw");

            if (grilled != null && raw != null)
            {
            grilled.gameObject.SetActive(true);
            raw.gameObject.SetActive(false);
            }
        }
    }
}
