using UnityEngine;
using UnityEditor;

public class MeshCombinerEditor
{
    [MenuItem("GameObject/Combine Meshes", false, 30)]
    public static void CombineMeshesInEditor()
    {
        GameObject selectedObject = Selection.activeGameObject;
        if (selectedObject)
        {
            MeshFilter[] meshFilters = selectedObject.GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];

            for (int i = 0; i < meshFilters.Length; i++)
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                meshFilters[i].gameObject.SetActive(false);
            }

            MeshFilter meshFilter = selectedObject.GetComponent<MeshFilter>();
            if (!meshFilter)
            {
                meshFilter = selectedObject.AddComponent<MeshFilter>();
            }

            meshFilter.mesh = new Mesh();
            meshFilter.mesh.CombineMeshes(combine);
            selectedObject.gameObject.SetActive(true);
        }
    }
}
