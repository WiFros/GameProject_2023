using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float pickupRange = 3f;
    [SerializeField] private LayerMask itemLayer;
    [SerializeField] private TextMeshProUGUI actionText;
    private Inventory inventory;
    private ItemPickUp currentItem;

    private void Start()
    {
        inventory = GetComponent<Inventory>();
    }
    private void Update()
    {
        CheckForItem();
        PickupItem();
    }

    private void CheckForItem()
    {
        RaycastHit hit;
        bool itemInRange = Physics.Raycast(transform.position, transform.forward, out hit, pickupRange, itemLayer);
        if (itemInRange)
        {
            ItemPickUp itemPickUp = hit.collider.GetComponent<ItemPickUp>();
            if (itemPickUp != null)
            {
                currentItem = itemPickUp;
                actionText.gameObject.SetActive(true);
                actionText.text = currentItem.item.itemName + " Get " + "<color=yellow>" + "(E)" + "</color>";
            }
        }
        else if (currentItem != null)
        {
            float distance = Vector3.Distance(transform.position, currentItem.transform.position);
            if (distance > pickupRange)
            {
                currentItem = null;
                actionText.gameObject.SetActive(false);
            }
        }
    }

    private void PickupItem()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentItem != null)
        {
            Debug.Log(currentItem.item.itemName + " 획득했습니다.");
            Destroy(currentItem.gameObject);
            actionText.gameObject.SetActive(false);
            currentItem = null;
        }
    }
    
}
