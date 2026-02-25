using UnityEngine;

public class GameObjectFollowScript : MonoBehaviour
{
    public GameObject hand;

    public void SetHand(GameObject handObject)
    {
        hand = handObject;
    }
    void Update()
    {
        if (hand != null)
        {
             transform.position = hand.transform.position;
        }
    }
}
