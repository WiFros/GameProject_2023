using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static bool invectoryActivated = false;  // 인벤토리 활성화 여부. true가 되면 카메라 움직임과 다른 입력을 막을 것이다.

    [SerializeField]
    private GameObject go_InventoryBase; // Inventory_Base 이미지
    [SerializeField] 
    private GameObject go_SlotsParent;  // Slot들의 부모인 Grid Setting 
    private Slot[] slots;  // 슬롯들 배열
    [SerializeField] private Button discardButton;
    private Slot selectedSlot;
    private Dictionary<string, Slot> inventorySlots = new Dictionary<string, Slot>();
    public static Inventory Instance { get; private set; }  // 싱글톤 인스턴스
    void Awake() 
    {
        // 싱글톤 구현부분
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        //discardButton.onClick.AddListener(() => DiscardItem());
    }
    void Update()
    {
        TryOpenInventory();
    }
    public void SetSelectedSlot(Slot _slot)
    {
        selectedSlot = _slot;
        //selectedSlot.
    }
    public void DiscardItem()
    {
        if (selectedSlot != null && selectedSlot.item != null)
        {
            Debug.Log("Discard : "+selectedSlot.item.itemName);
            //FindObjectOfType<InputNumber>().Call();
            selectedSlot.OnClickedDrop();
        }
    }
    
    private void TryOpenInventory()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            invectoryActivated = !invectoryActivated;

            if (invectoryActivated)
                OpenInventory();
            else
                CloseInventory();

        }
    }

    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        /*if(Item.ItemType.Equipment != _item.itemType)
        {
            if (inventorySlots.ContainsKey(_item.ID))
            {
                inventorySlots[_item.ID].SetSlotCount(_count);
                return;
            }
        }*/
        
        if(Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)  // null 이라면 slots[i].item.itemName 할 때 런타임 에러 나서
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }
    public bool HasEnoughItems(Item item, int requiredAmount)
    {
        /*int count = 0;

        foreach (var slot in slots)
        {
            if (slot.item != null && slot.item.itemName == itemName)
            {
                count += slot.GetSlotItemCount();
            }
        }

        return count >= requiredAmount;*/
        return inventorySlots.ContainsKey(item.ID) && inventorySlots[item.ID].GetSlotItemCount() >= requiredAmount;
    }
    public bool CheckItem(Item item, int amount)
    {
        return HasEnoughItems(item, amount);
    }

    public void RemoveItem(Item item, int amount)
    {
        if(HasEnoughItems(item, amount)) {
            inventorySlots[item.ID].SetSlotCount(-amount);
            
            if(inventorySlots[item.ID].GetSlotItemCount() <= 0) {
                inventorySlots.Remove(item.ID);
            }
        }
        else {
            Debug.Log("Not enough items to remove.");
        }
    }
    
}
