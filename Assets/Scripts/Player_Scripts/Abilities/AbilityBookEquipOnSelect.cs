using System;
using UnityEngine;

public class AbilityBookEquipOnSelect : MonoBehaviour
{
    [SerializeField] private AbilityInventory abilityInventory;
    [SerializeField] private GameObject fireBookPrefab;
    [SerializeField] private Transform rightHandAnchor;
    [SerializeField] private Transform leftHandAnchor;
    [SerializeField] private bool equipInRightHand = true;

    [Header("Local Transform")]
    [SerializeField] private Vector3 localPosition;
    [SerializeField] private Vector3 localRotationEuler;
    [SerializeField] private Vector3 localScale = Vector3.one;

    private GameObject equippedBook;

    public GameObject EquippedBook => equippedBook;
    public bool IsEquippedInRightHand => equipInRightHand;

    public event Action<GameObject, bool> OnBookInstanceChanged;

    public void SetPreferredHand(bool preferRightHand)
    {
        equipInRightHand = preferRightHand;

        if (abilityInventory != null && abilityInventory.CurrentAbility == AbilityType.FireBook)
        {
            EquipBook();
        }

        NotifyBookStateChanged();
    }

    private void Reset()
    {
        if (abilityInventory == null)
        {
            abilityInventory = GetComponent<AbilityInventory>();
        }
    }

    private void OnEnable()
    {
        if (abilityInventory != null)
        {
            abilityInventory.OnCurrentAbilityChanged += OnCurrentAbilityChanged;
            ApplyAbility(abilityInventory.CurrentAbility);
        }
    }

    private void OnDisable()
    {
        if (abilityInventory != null)
        {
            abilityInventory.OnCurrentAbilityChanged -= OnCurrentAbilityChanged;
        }
    }

    private void OnCurrentAbilityChanged(AbilityType newAbility)
    {
        ApplyAbility(newAbility);
    }

    private void ApplyAbility(AbilityType ability)
    {
        if (ability == AbilityType.FireBook)
        {
            EquipBook();
            return;
        }

        UnequipBook();
    }

    private void EquipBook()
    {
        Transform targetHand = equipInRightHand ? rightHandAnchor : leftHandAnchor;

        if (targetHand == null)
        {
            Debug.LogWarning($"No hand anchor assigned on {name}. Cannot equip FireBook.");
            return;
        }

        if (fireBookPrefab == null)
        {
            Debug.LogWarning($"No fireBookPrefab assigned on {name}.");
            return;
        }

        if (equippedBook != null)
        {
            equippedBook.transform.SetParent(targetHand, false);
            ApplyLocalTransform(equippedBook.transform);
            NotifyBookStateChanged();
            return;
        }

        equippedBook = Instantiate(fireBookPrefab, targetHand);
        equippedBook.name = "book";

        OpenCloseBook[] legacyBookControllers = equippedBook.GetComponentsInChildren<OpenCloseBook>(true);
        for (int i = 0; i < legacyBookControllers.Length; i++)
        {
            legacyBookControllers[i].enabled = false;
        }

        ApplyLocalTransform(equippedBook.transform);
        NotifyBookStateChanged();
    }

    private void UnequipBook()
    {
        if (equippedBook == null)
        {
            return;
        }

        Destroy(equippedBook);
        equippedBook = null;
        NotifyBookStateChanged();
    }

    private void ApplyLocalTransform(Transform target)
    {
        target.localPosition = localPosition;
        target.localRotation = Quaternion.Euler(localRotationEuler);
        target.localScale = localScale;
    }

    private void NotifyBookStateChanged()
    {
        OnBookInstanceChanged?.Invoke(equippedBook, equipInRightHand);
    }
}
