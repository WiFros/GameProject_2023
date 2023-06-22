using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Equipment,
        Used,
        Ingredient,
        Consumable,
        Quest,
        Etc
    }
    public string ID { get; private set; }
    public string itemName;
    public ItemType itemType;
    public Sprite itemImage;
    public GameObject itemPrefab;
    public string description;

    private void Awake()
    {
        ID = System.Guid.NewGuid().ToString();
    }
    public virtual void Use()
    {
        Debug.Log(itemName + "을 사용합니다.");
    }
}
