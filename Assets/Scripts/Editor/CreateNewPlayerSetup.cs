using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class CreateNewPlayerSetup
{
    private const string LegacyPlayerPrefabPath = "Assets/Prefabs/Player/Player.prefab";
    private const string NewPlayerPrefabPath = "Assets/Prefabs/Player/Player_NewCore.prefab";
    private const string FallbackStarterPrefabPath = "Assets/Samples/XR Interaction Toolkit/3.2.2/Starter Assets/Prefabs/XR Origin (XR Rig).prefab";
    private const string FireBookPrefabPath = "Assets/Prefabs/Abilities/FireBook.prefab";
    private const string LeftHandPrefabPath = "Assets/Animated Hands/Prefabs/Left Hand Model.prefab";
    private const string RightHandPrefabPath = "Assets/Animated Hands/Prefabs/Right Hand Model.prefab";

    [MenuItem("Tools/AnimaTua/Create New Core Player Setup")]
    public static void CreateSetup()
    {
        GameObject playerPrefab = LoadPlayerPrefab();

        if (playerPrefab == null)
        {
            Debug.LogError("No player prefab found. Expected Assets/Prefabs/Player/Player_NewCore.prefab or the XRI starter prefab.");
            return;
        }

        GameObject playerInstance = (GameObject)PrefabUtility.InstantiatePrefab(playerPrefab);
        playerInstance.name = "Player_NewCore_Instance";

        ApplyAnimatedHands(playerInstance);

        EditorSceneManager.MarkSceneDirty(playerInstance.scene);

        Selection.activeObject = playerInstance;

        Debug.Log("Created core-only player setup. Use Tools/AnimaTua/Create New Ability Module to add abilities separately.");
    }

    [MenuItem("Tools/AnimaTua/Apply Animated Hands To Selected Player")]
    public static void ApplyAnimatedHandsToSelectedPlayer()
    {
        GameObject playerInstance = Selection.activeGameObject;
        if (playerInstance == null)
        {
            Debug.LogError("Select a player root object first.");
            return;
        }

        ApplyAnimatedHands(playerInstance);
        EditorSceneManager.MarkSceneDirty(playerInstance.scene);
        Debug.Log("Applied animated hand visuals to selected player.");
    }

    [MenuItem("Tools/AnimaTua/Create New Ability Module")]
    public static void CreateAbilityModule()
    {
        GameObject playerInstance = Selection.activeGameObject;
        if (playerInstance == null)
        {
            Debug.LogError("Select the new player instance first, then run Create New Ability Module.");
            return;
        }

        GameObject systemsRoot = new GameObject("PlayerSystems_NewPlayer");

        GameObject abilityModule = new GameObject("AbilityModule");
        abilityModule.transform.SetParent(systemsRoot.transform, false);

        AbilityInventory inventory = abilityModule.AddComponent<AbilityInventory>();
        AbilityBookEquipOnSelect bookEquip = abilityModule.AddComponent<AbilityBookEquipOnSelect>();
        AbilityRotateInput rotateInput = abilityModule.AddComponent<AbilityRotateInput>();
        AbilityFireBookController fireBookController = abilityModule.AddComponent<AbilityFireBookController>();
        AbilityTeleportController teleportController = abilityModule.AddComponent<AbilityTeleportController>();

        Transform rightHand = FindTransform(playerInstance.transform, new[]
        {
            "Right Hand Controller",
            "RightHand Controller",
            "Right Controller",
            "RightHand"
        });

        Transform leftHand = FindTransform(playerInstance.transform, new[]
        {
            "Left Hand Controller",
            "LeftHand Controller",
            "Left Controller",
            "LeftHand"
        });

        Transform rightTeleportInteractor = FindTransformUnder(rightHand, new[] { "Teleport Interactor" });
        Transform leftTeleportInteractor = FindTransformUnder(leftHand, new[] { "Teleport Interactor" });

        SetObjectReference(bookEquip, "abilityInventory", inventory);
        SetObjectReference(bookEquip, "fireBookPrefab", AssetDatabase.LoadAssetAtPath<GameObject>(FireBookPrefabPath));
        SetObjectReference(bookEquip, "rightHandAnchor", rightHand);
        SetObjectReference(bookEquip, "leftHandAnchor", leftHand);

        SetObjectReference(rotateInput, "abilityInventory", inventory);
        SetObjectReference(rotateInput, "bookEquipOnSelect", bookEquip);

        SetObjectReference(fireBookController, "abilityInventory", inventory);
        SetObjectReference(fireBookController, "bookEquip", bookEquip);
        SetObjectReference(fireBookController, "rightHandAnchor", rightHand);
        SetObjectReference(fireBookController, "leftHandAnchor", leftHand);

        Transform rightShooter = FindShooterRoot(rightHand);
        Transform leftShooter = FindShooterRoot(leftHand);
        SetObjectReference(fireBookController, "rightHandShooterRoot", rightShooter);
        SetObjectReference(fireBookController, "leftHandShooterRoot", leftShooter);

        SetObjectReference(teleportController, "abilityInventory", inventory);
        SetObjectReference(teleportController, "rightTeleportInteractorObject", rightTeleportInteractor != null ? rightTeleportInteractor.gameObject : null);
        SetObjectReference(teleportController, "leftTeleportInteractorObject", leftTeleportInteractor != null ? leftTeleportInteractor.gameObject : null);
        SetObjectReference(teleportController, "rightHandAnchor", rightHand);
        SetObjectReference(teleportController, "leftHandAnchor", leftHand);

        if (rightShooter == null || leftShooter == null)
        {
            Debug.LogWarning("Could not auto-detect FireHandShootScript on one or both hands. Assign rightHandShooterRoot/leftHandShooterRoot manually on AbilityFireBookController.");
        }

        if (rightTeleportInteractor == null || leftTeleportInteractor == null)
        {
            Debug.LogWarning("Could not auto-detect one or both Teleport Interactors. Assign them manually on AbilityTeleportController.");
        }

        EditorSceneManager.MarkSceneDirty(playerInstance.scene);

        Selection.objects = new Object[] { playerInstance, systemsRoot, abilityModule };

        Debug.Log("Created modular player setup. Assign on AbilityModule: fireBookPrefab, firePrefab, B/Y rotate, and left/right interact actions. Teleport is auto-wired if Teleport Interactors were found.");
    }

    private static GameObject LoadPlayerPrefab()
    {
        GameObject playerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(NewPlayerPrefabPath);
        if (playerPrefab == null)
        {
            playerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(FallbackStarterPrefabPath);
        }

        return playerPrefab;
    }

    private static void ApplyAnimatedHands(GameObject playerInstance)
    {
        Transform rightController = FindTransform(playerInstance.transform, new[]
        {
            "Right Controller",
            "Right Hand Controller",
            "RightHand Controller",
            "RightHand"
        });

        Transform leftController = FindTransform(playerInstance.transform, new[]
        {
            "Left Controller",
            "Left Hand Controller",
            "LeftHand Controller",
            "LeftHand"
        });

        if (rightController == null || leftController == null)
        {
            Debug.LogWarning("Could not find left/right controller transforms for animated hands.");
            return;
        }

        SetActiveByName(rightController, "Right Controller Visual", false);
        SetActiveByName(leftController, "Left Controller Visual", false);

        bool handsSpawnedFromLegacy = false;
        GameObject legacyPlayer = AssetDatabase.LoadAssetAtPath<GameObject>(LegacyPlayerPrefabPath);
        if (legacyPlayer != null)
        {
            GameObject legacyInstance = (GameObject)PrefabUtility.InstantiatePrefab(legacyPlayer);
            if (legacyInstance != null)
            {
                try
                {
                    bool rightOk = SpawnHandModelFromLegacy(legacyInstance.transform, "Right Hand Model", rightController);
                    bool leftOk = SpawnHandModelFromLegacy(legacyInstance.transform, "Left Hand Model", leftController);
                    handsSpawnedFromLegacy = rightOk && leftOk;
                }
                finally
                {
                    Object.DestroyImmediate(legacyInstance);
                }
            }
        }

        if (!handsSpawnedFromLegacy)
        {
            GameObject rightHand = SpawnHandPrefab(RightHandPrefabPath, rightController, "Right Hand Model");
            GameObject leftHand = SpawnHandPrefab(LeftHandPrefabPath, leftController, "Left Hand Model");
            ApplyLegacyHandAnimationBindings(rightHand, leftHand);
        }
    }

    private static bool SpawnHandModelFromLegacy(Transform legacyRoot, string handName, Transform targetController)
    {
        Transform legacyHand = FindTransform(legacyRoot, new[] { handName });
        if (legacyHand == null)
        {
            return false;
        }

        Transform existing = targetController.Find(handName);
        if (existing != null)
        {
            Object.DestroyImmediate(existing.gameObject);
        }

        GameObject handClone = Object.Instantiate(legacyHand.gameObject, targetController, false);
        handClone.name = handName;
        handClone.SetActive(true);
        return true;
    }

    private static GameObject SpawnHandPrefab(string prefabPath, Transform parent, string instanceName)
    {
        GameObject handPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        if (handPrefab == null)
        {
            Debug.LogWarning($"Could not load hand prefab at {prefabPath}.");
            return null;
        }

        Transform existing = parent.Find(instanceName);
        if (existing != null)
        {
            Object.DestroyImmediate(existing.gameObject);
        }

        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(handPrefab);
        if (instance == null)
        {
            instance = Object.Instantiate(handPrefab);
        }

        instance.name = instanceName;
        instance.transform.SetParent(parent, false);

        return instance;
    }

    private static void ApplyLegacyHandAnimationBindings(GameObject rightHand, GameObject leftHand)
    {
        if (rightHand == null || leftHand == null)
        {
            return;
        }

        GameObject legacyPlayer = AssetDatabase.LoadAssetAtPath<GameObject>(LegacyPlayerPrefabPath);
        if (legacyPlayer == null)
        {
            Debug.LogWarning("Legacy Player prefab not found. Hand models were spawned without old input bindings.");
            return;
        }

        GameObject legacyInstance = (GameObject)PrefabUtility.InstantiatePrefab(legacyPlayer);
        if (legacyInstance == null)
        {
            Debug.LogWarning("Could not instantiate legacy Player prefab for hand binding copy.");
            return;
        }

        try
        {
            AnimateHandOnInput legacyRight = FindTransform(legacyInstance.transform, new[] { "Right Hand Model" })?.GetComponent<AnimateHandOnInput>();
            AnimateHandOnInput legacyLeft = FindTransform(legacyInstance.transform, new[] { "Left Hand Model" })?.GetComponent<AnimateHandOnInput>();

            SkinnedMeshRenderer legacyRightRenderer = FindTransform(legacyInstance.transform, new[] { "Right Hand Model" })?.GetComponentInChildren<SkinnedMeshRenderer>(true);
            SkinnedMeshRenderer legacyLeftRenderer = FindTransform(legacyInstance.transform, new[] { "Left Hand Model" })?.GetComponentInChildren<SkinnedMeshRenderer>(true);

            SkinnedMeshRenderer newRightRenderer = rightHand.GetComponentInChildren<SkinnedMeshRenderer>(true);
            SkinnedMeshRenderer newLeftRenderer = leftHand.GetComponentInChildren<SkinnedMeshRenderer>(true);

            Animator rightAnimator = rightHand.GetComponent<Animator>();
            Animator leftAnimator = leftHand.GetComponent<Animator>();

            AnimateHandOnInput newRight = rightHand.GetComponent<AnimateHandOnInput>();
            if (newRight == null)
            {
                newRight = rightHand.AddComponent<AnimateHandOnInput>();
            }

            AnimateHandOnInput newLeft = leftHand.GetComponent<AnimateHandOnInput>();
            if (newLeft == null)
            {
                newLeft = leftHand.AddComponent<AnimateHandOnInput>();
            }

            if (rightAnimator != null)
            {
                SetObjectReference(newRight, "handAnimator", rightAnimator);
            }

            if (leftAnimator != null)
            {
                SetObjectReference(newLeft, "handAnimator", leftAnimator);
            }

            if (legacyRightRenderer != null && newRightRenderer != null)
            {
                newRightRenderer.sharedMaterials = legacyRightRenderer.sharedMaterials;
            }

            if (legacyLeftRenderer != null && newLeftRenderer != null)
            {
                newLeftRenderer.sharedMaterials = legacyLeftRenderer.sharedMaterials;
            }

            CopyInputActionProperty(legacyRight, newRight, "triggerValue");
            CopyInputActionProperty(legacyRight, newRight, "gripValue");
            CopyInputActionProperty(legacyLeft, newLeft, "triggerValue");
            CopyInputActionProperty(legacyLeft, newLeft, "gripValue");
        }
        finally
        {
            Object.DestroyImmediate(legacyInstance);
        }
    }

    private static void CopyInputActionProperty(Object source, Object target, string propertyName)
    {
        if (source == null || target == null)
        {
            return;
        }

        SerializedObject sourceObject = new SerializedObject(source);
        SerializedObject targetObject = new SerializedObject(target);

        SerializedProperty sourceRoot = sourceObject.FindProperty(propertyName);
        SerializedProperty targetRoot = targetObject.FindProperty(propertyName);

        if (sourceRoot == null || targetRoot == null)
        {
            return;
        }

        SerializedProperty sourceUseReference = sourceRoot.FindPropertyRelative("m_UseReference");
        SerializedProperty sourceReference = sourceRoot.FindPropertyRelative("m_Reference");
        SerializedProperty sourceAction = sourceRoot.FindPropertyRelative("m_Action");

        SerializedProperty targetUseReference = targetRoot.FindPropertyRelative("m_UseReference");
        SerializedProperty targetReference = targetRoot.FindPropertyRelative("m_Reference");
        SerializedProperty targetAction = targetRoot.FindPropertyRelative("m_Action");

        if (sourceUseReference != null && targetUseReference != null)
        {
            targetUseReference.intValue = sourceUseReference.intValue;
        }

        if (sourceReference != null && targetReference != null)
        {
            targetReference.objectReferenceValue = sourceReference.objectReferenceValue;
        }

        if (sourceAction != null && targetAction != null)
        {
            targetAction.FindPropertyRelative("m_Name").stringValue = sourceAction.FindPropertyRelative("m_Name").stringValue;
            targetAction.FindPropertyRelative("m_Type").intValue = sourceAction.FindPropertyRelative("m_Type").intValue;
            targetAction.FindPropertyRelative("m_ExpectedControlType").stringValue = sourceAction.FindPropertyRelative("m_ExpectedControlType").stringValue;
            targetAction.FindPropertyRelative("m_Id").stringValue = sourceAction.FindPropertyRelative("m_Id").stringValue;
        }

        targetObject.ApplyModifiedPropertiesWithoutUndo();
    }

    private static void SetActiveByName(Transform root, string objectName, bool active)
    {
        Transform[] children = root.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i].name == objectName)
            {
                children[i].gameObject.SetActive(active);
            }
        }
    }

    private static Transform FindTransform(Transform root, string[] names)
    {
        if (root == null)
        {
            return null;
        }

        Transform[] children = root.GetComponentsInChildren<Transform>(true);

        foreach (string expectedName in names)
        {
            Transform exact = children.FirstOrDefault(t => t.name == expectedName);
            if (exact != null)
            {
                return exact;
            }
        }

        return null;
    }

    private static Transform FindTransformUnder(Transform root, string[] names)
    {
        return FindTransform(root, names);
    }

    private static void SetObjectReference(Object target, string propertyName, Object value)
    {
        SerializedObject serializedObject = new SerializedObject(target);
        SerializedProperty prop = serializedObject.FindProperty(propertyName);
        if (prop != null)
        {
            prop.objectReferenceValue = value;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }
    }

    private static Transform FindShooterRoot(Transform handRoot)
    {
        if (handRoot == null)
        {
            return null;
        }

        FireHandShootScript[] shooters = handRoot.GetComponentsInChildren<FireHandShootScript>(true);
        if (shooters != null && shooters.Length > 0)
        {
            return shooters[0].transform;
        }

        return null;
    }
}
