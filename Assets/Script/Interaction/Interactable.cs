using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject interactionUI;
    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private string interactionMessage;
    private bool isPlayerInRange = false;
    
    public void ShowUI()
    {
        interactionUI.SetActive(true);
        interactionText.text = interactionMessage;
    }

    public void HideUI()
    {
        interactionUI.SetActive(false);
    }

    public bool CanInteract()
    {
        return isPlayerInRange;
    }

    public abstract void Interact(GameObject player);
}
