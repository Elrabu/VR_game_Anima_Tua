using UnityEngine;

public class IngredientsResetScript : MonoBehaviour
{
    [SerializeField] private GameObject breadBottom;
    [SerializeField] private GameObject patty;
    [SerializeField] private GameObject breadTop;
    [SerializeField] private GameObject pickels;

    [SerializeField] private Transform breadBottomSpawn;
    [SerializeField] private Transform pattySpawn;
    [SerializeField] private Transform breadTopSpawn;
    [SerializeField] private Transform pickelsSpawn;
    [SerializeField] private void ResetIngredient()
    {
        Instantiate(breadBottom, breadBottomSpawn.position, breadBottomSpawn.rotation);
        Instantiate(patty, pattySpawn.position, pattySpawn.rotation);
        Instantiate(breadTop, breadTopSpawn.position, breadTopSpawn.rotation);
        Instantiate(pickels, pickelsSpawn.position, pickelsSpawn.rotation);
    }
}