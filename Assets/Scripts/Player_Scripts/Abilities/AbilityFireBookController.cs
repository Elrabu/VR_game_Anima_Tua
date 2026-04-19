using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityFireBookController : MonoBehaviour
{
    [SerializeField] private AbilityInventory abilityInventory;
    [SerializeField] private AbilityBookEquipOnSelect bookEquip;

    [Header("Book Input")]
    [SerializeField] private InputActionReference interactLeft;
    [SerializeField] private InputActionReference interactRight;
    [SerializeField] private float pressedThreshold = 0.5f;
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private bool autoEnableInteractActions = true;
    [SerializeField] private bool verboseDebug = false;

    [Header("Fire Visuals")]
    [SerializeField] private Transform rightHandAnchor;
    [SerializeField] private Transform leftHandAnchor;
    [SerializeField] private Transform rightHandShooterRoot;
    [SerializeField] private Transform leftHandShooterRoot;
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private AudioClip fireStartSound;

    private Animator bookAnimator;
    private GameObject currentFire;
    private AudioSource ignitionSource;
    private float cooldownTimer;
    private bool fireActive;
    private bool lastBookInRightHand;

    private void Reset()
    {
        if (abilityInventory == null)
        {
            abilityInventory = GetComponent<AbilityInventory>();
        }

        if (bookEquip == null)
        {
            bookEquip = GetComponent<AbilityBookEquipOnSelect>();
        }
    }

    private void OnEnable()
    {
        if (autoEnableInteractActions)
        {
            TryEnableAction(interactLeft);
            TryEnableAction(interactRight);
        }

        if (abilityInventory != null)
        {
            abilityInventory.OnCurrentAbilityChanged += OnAbilityChanged;
        }

        if (bookEquip != null)
        {
            bookEquip.OnBookInstanceChanged += OnBookInstanceChanged;
            OnBookInstanceChanged(bookEquip.EquippedBook, bookEquip.IsEquippedInRightHand);
        }

        if (abilityInventory != null)
        {
            OnAbilityChanged(abilityInventory.CurrentAbility);
        }
    }

    private void OnDisable()
    {
        if (autoEnableInteractActions)
        {
            TryDisableAction(interactLeft);
            TryDisableAction(interactRight);
        }

        if (abilityInventory != null)
        {
            abilityInventory.OnCurrentAbilityChanged -= OnAbilityChanged;
        }

        if (bookEquip != null)
        {
            bookEquip.OnBookInstanceChanged -= OnBookInstanceChanged;
        }

        ForceCleanup();
    }

    private void Update()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (!IsFireBookActive())
        {
            return;
        }

        if (bookAnimator == null)
        {
            return;
        }

        bool bookInRightHand = bookEquip != null && bookEquip.IsEquippedInRightHand;

        float pressedValue = bookInRightHand
            ? ReadAction(interactRight)
            : ReadAction(interactLeft);

        if (pressedValue <= pressedThreshold || cooldownTimer > 0f)
        {
            return;
        }

        ToggleBookState(bookInRightHand);
        cooldownTimer = cooldown;
    }

    private void OnAbilityChanged(AbilityType ability)
    {
        if (ability == AbilityType.FireBook)
        {
            if (bookEquip != null)
            {
                OnBookInstanceChanged(bookEquip.EquippedBook, bookEquip.IsEquippedInRightHand);
            }

            return;
        }

        ForceCleanup();
    }

    private void OnBookInstanceChanged(GameObject bookInstance, bool equippedInRightHand)
    {
        if (bookInstance == null)
        {
            bookAnimator = null;
            ForceCleanup();
            lastBookInRightHand = equippedInRightHand;
            return;
        }

        Animator animator = bookInstance.GetComponentInChildren<Animator>(true);
        if (animator != null)
        {
            bookAnimator = animator;
        }

        if (lastBookInRightHand != equippedInRightHand)
        {
            DespawnFire();
        }

        lastBookInRightHand = equippedInRightHand;
    }

    private void ToggleBookState(bool bookInRightHand)
    {
        if (!bookAnimator.GetBool("IsOpen"))
        {
            bookAnimator.SetTrigger("Open");
            bookAnimator.SetBool("IsOpen", true);
            SpawnFire(bookInRightHand);
            return;
        }

        bookAnimator.SetTrigger("Close");
        bookAnimator.SetBool("IsOpen", false);
        DespawnFire();
    }

    private void SpawnFire(bool bookInRightHand)
    {
        if (fireActive)
        {
            return;
        }

        Transform effectHand = bookInRightHand ? leftHandAnchor : rightHandAnchor;
        Transform shooterRoot = bookInRightHand ? leftHandShooterRoot : rightHandShooterRoot;

        if (effectHand == null)
        {
            Debug.LogWarning("FireBookController: Missing hand anchor reference.");
            return;
        }

        if (firePrefab != null)
        {
            currentFire = Instantiate(firePrefab, effectHand.position, effectHand.rotation);
            GameObjectFollowScript follow = currentFire.GetComponent<GameObjectFollowScript>();
            if (follow != null)
            {
                follow.SetHand(effectHand.gameObject);
            }
        }

        if (shooterRoot != null)
        {
            shooterRoot.gameObject.SetActive(true);
            if (verboseDebug)
            {
                Debug.Log($"FireBookController enabled shooter root: {shooterRoot.name}");
            }
        }
        else if (verboseDebug)
        {
            Debug.LogWarning("FireBookController: shooterRoot is null, so no fireball shooter gets activated.");
        }

        AudioSource source = effectHand.GetComponentInParent<AudioSource>();
        if (source != null && fireStartSound != null)
        {
            ignitionSource = source;
            ignitionSource.PlayOneShot(fireStartSound);
        }

        fireActive = true;
    }

    private void DespawnFire()
    {
        if (ignitionSource != null)
        {
            ignitionSource.Stop();
            ignitionSource = null;
        }

        if (currentFire != null)
        {
            ParticleSystem particleSystem = currentFire.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                var psMain = particleSystem.main;
                psMain.stopAction = ParticleSystemStopAction.Destroy;
                particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
            else
            {
                Destroy(currentFire);
            }

            currentFire = null;
        }

        if (leftHandShooterRoot != null)
        {
            leftHandShooterRoot.gameObject.SetActive(false);
        }

        if (rightHandShooterRoot != null)
        {
            rightHandShooterRoot.gameObject.SetActive(false);
        }

        fireActive = false;
    }

    private void ForceCleanup()
    {
        DespawnFire();

        if (bookAnimator != null)
        {
            bookAnimator.SetBool("IsOpen", false);
            bookAnimator.SetTrigger("Close");
        }
    }

    private bool IsFireBookActive()
    {
        return abilityInventory != null && abilityInventory.CurrentAbility == AbilityType.FireBook;
    }

    private float ReadAction(InputActionReference actionReference)
    {
        if (actionReference == null || actionReference.action == null)
        {
            return 0f;
        }

        return actionReference.action.ReadValue<float>();
    }

    private void TryEnableAction(InputActionReference actionReference)
    {
        if (actionReference == null || actionReference.action == null)
        {
            return;
        }

        if (!actionReference.action.enabled)
        {
            actionReference.action.Enable();
        }
    }

    private void TryDisableAction(InputActionReference actionReference)
    {
        if (actionReference == null || actionReference.action == null)
        {
            return;
        }

        if (actionReference.action.enabled)
        {
            actionReference.action.Disable();
        }
    }
}
