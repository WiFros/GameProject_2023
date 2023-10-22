using UnityEngine;
using UnityEditor;

public class RemoveColliders
{
    [MenuItem("Custom/Remove Colliders")]
        static void RemoveAllColliders()
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                Collider[] colliders = obj.GetComponentsInChildren<Collider>();
                foreach (Collider collider in colliders)
                {
                    Object.DestroyImmediate(collider);
                }
            }
        }
}
