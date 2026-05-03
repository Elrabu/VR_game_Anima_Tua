using UnityEngine;

public class UnlockBook : MonoBehaviour
{
    [SerializeField] private AbilityBookEquipOnSelect abilityBookEquipOnSelect;
    [SerializeField] private AbilityRotateInput abilityRotateInput;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnableInventory();
        }
    }

    private void EnableInventory()
    {
        if (abilityBookEquipOnSelect != null)
        {
            abilityBookEquipOnSelect.enabled = true;
        }

        if (abilityRotateInput != null)
        {
            abilityRotateInput.enabled = true;
        }
    }
}
