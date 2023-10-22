#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using TMPro;

public class TMPFontAssetChangerForSelected : EditorWindow
{
    private TMP_FontAsset targetFontAsset;

    [MenuItem("Tools/Change TMP Font Asset for Selected Object's Children")]
    public static void ShowWindow()
    {
        GetWindow<TMPFontAssetChangerForSelected>("Change TMP Font Asset for Selected Object's Children");
    }

    private void OnGUI()
    {
        targetFontAsset = (TMP_FontAsset)EditorGUILayout.ObjectField("Target Font Asset", targetFontAsset, typeof(TMP_FontAsset), false);

        if (GUILayout.Button("Change Font Asset"))
        {
            ChangeFontAssetForSelected();
        }
    }

    private void ChangeFontAssetForSelected()
    {
        if (targetFontAsset == null)
        {
            Debug.LogError("Target Font Asset is not set!");
            return;
        }

        if (Selection.activeGameObject == null)
        {
            Debug.LogError("No object selected!");
            return;
        }

        TextMeshProUGUI[] allTexts = Selection.activeGameObject.GetComponentsInChildren<TextMeshProUGUI>(true);
        foreach (TextMeshProUGUI text in allTexts)
        {
            text.font = targetFontAsset;
            EditorUtility.SetDirty(text.gameObject);
        }

        Debug.Log("Font Asset changed for all TextMeshPro objects in the selected object's children!");
    }
}
#endif