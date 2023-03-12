using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushInteraction : MonoBehaviour
{
    public float interactionDistance = 2.0f;

    private GameObject currentObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && currentObject != null)
        {
            PushObject(currentObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            currentObject = other.gameObject;
            Debug.Log("current in");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            currentObject = null;
            Debug.Log("current out");
        }
    }
    void PushObject(GameObject currentObject)
    {

        PushableObject pushable = currentObject.GetComponent<PushableObject>();

        if (pushable != null)
        {
            Vector3 direction = transform.forward;
            
            pushable.Push(direction);
        }
    }
}
