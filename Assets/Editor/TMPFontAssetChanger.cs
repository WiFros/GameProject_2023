#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using TMPro;

public class TMPFontAssetChanger : EditorWindow
{
    private TMP_FontAsset targetFontAsset;
    private bool applyToActiveObjects = true;
    private bool applyToInactiveObjects = true;

    [MenuItem("Tools/Change TMP Font Asset in Scene Prefabs")]
    public static void ShowWindow()
    {
        GetWindow<TMPFontAssetChanger>("Change TMP Font Asset in Scene Prefabs");
    }

    private void OnGUI()
    {
        targetFontAsset = (TMP_FontAsset)EditorGUILayout.ObjectField("Target Font Asset", targetFontAsset, typeof(TMP_FontAsset), false);
        applyToActiveObjects = EditorGUILayout.Toggle("Apply to Active Objects", applyToActiveObjects);
        applyToInactiveObjects = EditorGUILayout.Toggle("Apply to Inactive Objects", applyToInactiveObjects);

        if (GUILayout.Button("Change Font Asset"))
        {
            ChangeFontAssetInScenePrefabs();
        }
    }

    private void ChangeFontAssetInScenePrefabs()
    {
        if (targetFontAsset == null)
        {
            Debug.LogError("Target Font Asset is not set!");
            return;
        }

        TextMeshProUGUI[] allTexts = FindObjectsOfType<TextMeshProUGUI>(applyToInactiveObjects);
        foreach (TextMeshProUGUI text in allTexts)
        {
            if (PrefabUtility.IsPartOfAnyPrefab(text.gameObject) && 
               ((applyToActiveObjects && text.gameObject.activeInHierarchy) || (applyToInactiveObjects && !text.gameObject.activeInHierarchy)))
            {
                text.font = targetFontAsset;
                EditorUtility.SetDirty(text.gameObject);
            }
        }

        Debug.Log("Font Asset changed for TextMeshPro objects in the current scene's prefabs based on selected options!");
    }
}
#endif
