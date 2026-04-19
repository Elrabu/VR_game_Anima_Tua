using UnityEditor;
using UnityEngine;

public static class CreateIndependentFireBookPrefab
{
    private const string SourceModelPath = "Assets/3D/Character/Book.fbx";
    private const string AnimatorControllerPath = "Assets/3D/Controllers/Book.controller";
    private const string OutputPrefabPath = "Assets/Prefabs/Abilities/FireBook.prefab";

    [MenuItem("Tools/AnimaTua/Create Independent FireBook Prefab")]
    public static void CreatePrefab()
    {
        GameObject sourceModel = AssetDatabase.LoadAssetAtPath<GameObject>(SourceModelPath);
        if (sourceModel == null)
        {
            Debug.LogError("Could not find source model at Assets/3D/Character/Book.fbx");
            return;
        }

        RuntimeAnimatorController animatorController = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>(AnimatorControllerPath);

        GameObject root = new GameObject("FireBook");
        root.tag = "Book";

        GameObject modelInstance = (GameObject)PrefabUtility.InstantiatePrefab(sourceModel);
        if (modelInstance == null)
        {
            modelInstance = Object.Instantiate(sourceModel);
        }

        modelInstance.name = "book";
        modelInstance.transform.SetParent(root.transform, false);
        modelInstance.transform.localPosition = Vector3.zero;
        modelInstance.transform.localRotation = Quaternion.identity;
        modelInstance.transform.localScale = Vector3.one;

        Animator animator = root.GetComponent<Animator>();
        if (animator == null)
        {
            animator = root.AddComponent<Animator>();
        }

        if (animatorController != null)
        {
            animator.runtimeAnimatorController = animatorController;
        }

        if (root.GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = root.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        if (root.GetComponent<Collider>() == null)
        {
            BoxCollider collider = root.AddComponent<BoxCollider>();
            collider.isTrigger = false;
            collider.size = new Vector3(0.2f, 0.25f, 0.08f);
            collider.center = new Vector3(0f, 0.12f, 0f);
        }

        PrefabUtility.SaveAsPrefabAsset(root, OutputPrefabPath);
        Object.DestroyImmediate(root);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Created independent FireBook prefab at Assets/Prefabs/Abilities/FireBook.prefab");
    }
}
