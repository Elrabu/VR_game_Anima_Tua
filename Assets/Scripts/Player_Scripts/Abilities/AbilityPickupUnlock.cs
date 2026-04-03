using UnityEngine;

public class AbilityPickupUnlock : MonoBehaviour
{
    [SerializeField] private AbilityType abilityToUnlock = AbilityType.Fire;
    [SerializeField] private AbilityInventory abilityInventory;
    [SerializeField] private bool unlockOnTriggerEnter = true;
    [SerializeField] private bool requirePlayerTag = true;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private bool destroyAfterUnlock = true;

    public void TryUnlock()
    {
        AbilityInventory targetInventory = abilityInventory;
        if (targetInventory == null)
        {
            targetInventory = FindFirstObjectByType<AbilityInventory>();
        }

        if (targetInventory == null)
        {
            Debug.LogWarning($"No AbilityInventory found for {name}.");
            return;
        }

        bool unlocked = targetInventory.UnlockAbility(abilityToUnlock);

        if (unlocked && destroyAfterUnlock)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!unlockOnTriggerEnter)
        {
            return;
        }

        if (requirePlayerTag && !other.CompareTag(playerTag))
        {
            return;
        }

        TryUnlock();
    }
}
