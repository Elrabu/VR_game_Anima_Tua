using UnityEngine;

public class GameObjectFollowScript : MonoBehaviour
{
    public GameObject hand;
    // Update is called once per frame
    void Update()
    {
        transform.position = hand.transform.position;
    }
}
