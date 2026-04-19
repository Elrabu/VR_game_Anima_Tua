using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityInventory : MonoBehaviour
{
    [SerializeField] private List<AbilityType> abilityOrder = new List<AbilityType>
    {
        AbilityType.FireBook,
        AbilityType.Teleport,
        AbilityType.TractorBeam,
    };

    [SerializeField] private List<AbilityType> startUnlocked = new List<AbilityType>();
    [SerializeField] private bool includeNoneInRotation = true;
    [SerializeField] private bool autoRemoveDuplicateAbilities = false;

    private readonly HashSet<AbilityType> unlockedAbilities = new HashSet<AbilityType>();
    private int currentIndex = -1;

    public AbilityType CurrentAbility { get; private set; } = AbilityType.None;

    public event Action<AbilityType> OnCurrentAbilityChanged;
    public event Action<AbilityType> OnAbilityUnlocked;

    private void Awake()
    {
        InitializeUnlockedAbilities();
        SelectFirstUnlockedAbility();
    }

    public bool UnlockAbility(AbilityType ability)
    {
        if (ability == AbilityType.None)
        {
            return false;
        }

        if (!abilityOrder.Contains(ability))
        {
            Debug.LogWarning($"Ability {ability} is not in the fixed ability order on {name}.");
            return false;
        }

        if (!unlockedAbilities.Add(ability))
        {
            return false;
        }

        OnAbilityUnlocked?.Invoke(ability);

        if (CurrentAbility == AbilityType.None)
        {
            SetCurrentAbility(abilityOrder.IndexOf(ability));
        }

        return true;
    }

    public bool HasAbility(AbilityType ability)
    {
        return unlockedAbilities.Contains(ability);
    }

    public bool SelectAbility(AbilityType ability)
    {
        if (ability == AbilityType.None)
        {
            SetCurrentAbility(-1);
            return true;
        }

        if (!unlockedAbilities.Contains(ability))
        {
            return false;
        }

        int abilityIndex = abilityOrder.IndexOf(ability);
        if (abilityIndex < 0)
        {
            Debug.LogWarning($"Ability {ability} is not in the fixed ability order on {name}.");
            return false;
        }

        SetCurrentAbility(abilityIndex);
        return true;
    }

    public bool RotateNextAbility()
    {
        if (unlockedAbilities.Count == 0 || abilityOrder.Count == 0)
        {
            SetCurrentAbility(-1);
            return false;
        }

        List<int> availableIndices = new List<int>();
        for (int i = 0; i < abilityOrder.Count; i++)
        {
            if (unlockedAbilities.Contains(abilityOrder[i]))
            {
                availableIndices.Add(i);
            }
        }

        if (includeNoneInRotation)
        {
            availableIndices.Add(-1);
        }

        if (availableIndices.Count == 0)
        {
            SetCurrentAbility(-1);
            return false;
        }

        int currentToken = CurrentAbility == AbilityType.None ? -1 : currentIndex;
        int currentPosition = availableIndices.IndexOf(currentToken);
        int nextPosition = (currentPosition + 1 + availableIndices.Count) % availableIndices.Count;
        int nextToken = availableIndices[nextPosition];

        AbilityType before = CurrentAbility;
        SetCurrentAbility(nextToken);

        return before != CurrentAbility;
    }

    private void InitializeUnlockedAbilities()
    {
        unlockedAbilities.Clear();

        for (int i = 0; i < startUnlocked.Count; i++)
        {
            AbilityType ability = startUnlocked[i];
            if (ability != AbilityType.None && abilityOrder.Contains(ability))
            {
                unlockedAbilities.Add(ability);
            }
        }
    }

    private void SelectFirstUnlockedAbility()
    {
        for (int i = 0; i < abilityOrder.Count; i++)
        {
            if (unlockedAbilities.Contains(abilityOrder[i]))
            {
                SetCurrentAbility(i, notify: false);
                return;
            }
        }

        SetCurrentAbility(-1, notify: false);
    }

    private void SetCurrentAbility(int newIndex, bool notify = true)
    {
        currentIndex = newIndex;

        AbilityType newAbility = AbilityType.None;
        if (newIndex >= 0 && newIndex < abilityOrder.Count)
        {
            newAbility = abilityOrder[newIndex];
        }

        if (CurrentAbility == newAbility)
        {
            return;
        }

        CurrentAbility = newAbility;

        if (notify)
        {
            OnCurrentAbilityChanged?.Invoke(CurrentAbility);
        }
    }

    private void OnValidate()
    {
        if (abilityOrder == null)
        {
            abilityOrder = new List<AbilityType>();
            return;
        }

        if (!autoRemoveDuplicateAbilities)
        {
            return;
        }

        HashSet<AbilityType> seen = new HashSet<AbilityType>();
        for (int i = abilityOrder.Count - 1; i >= 0; i--)
        {
            AbilityType current = abilityOrder[i];
            if (current == AbilityType.None)
            {
                continue;
            }

            if (!seen.Add(current))
            {
                abilityOrder.RemoveAt(i);
            }
        }
    }

    [ContextMenu("Reset Ability Order To Defaults")]
    private void ResetAbilityOrderToDefaults()
    {
        abilityOrder = new List<AbilityType>
        {
            AbilityType.FireBook,
            AbilityType.Teleport,
            AbilityType.TractorBeam,
        };
    }
}
