using UnityEngine;

public class ColissionTestingScript : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with " + collision.gameObject.name);
        //Destroy(gameObject);
    }
}
