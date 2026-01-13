using UnityEngine;

public class IngredientsResetScript : MonoBehaviour
{
    public GameObject breadBottom;
    public GameObject patty;
    public GameObject breadTop;
    public GameObject pickels;

    public Transform breadBottomSpawn;
    public Transform pattySpawn;
    public Transform breadTopSpawn;
    public Transform pickelsSpawn;
    public void ResetIngredient()
    {
        Instantiate(breadBottom, breadBottomSpawn.position, breadBottomSpawn.rotation);
        Instantiate(patty, pattySpawn.position, pattySpawn.rotation);
        Instantiate(breadTop, breadTopSpawn.position, breadTopSpawn.rotation);
        Instantiate(pickels, pickelsSpawn.position, pickelsSpawn.rotation);
    }
}