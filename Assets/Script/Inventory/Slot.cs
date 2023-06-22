using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler,IBeginDragHandler,IDragHandler,IEndDragHandler,IDropHandler
{
        public Item item; // 획득한 아이템
        public int itemCount; // 획득한 아이템의 개수
        public Image itemImage;  // 아이템의 이미지
        private Rect baseRect;
        [SerializeField]
        private TextMeshProUGUI text_Count;
        [SerializeField]
        private GameObject go_CountImage;
        public InputNumber theInputNumber;
        public Button dropButton;
        private Slot currentSlot = null;
        
        private void Start()
        {
            baseRect = transform.parent.parent.GetComponent<RectTransform>().rect;
            //theWeaponManager = FindObjectOfType<WeaponManager>();
            theInputNumber = FindObjectOfType<InputNumber>();
            //theWeaponManager = FindObjectOfType<WeaponManager>();
        }

        public void OnClickedDrop()
        {
            //Debug.Log("Drop1");
            if(SelectedSlot.instance.selectSlot != null)
            {
                theInputNumber = FindObjectOfType<InputNumber>();
                //Debug.Log("Drop2");
                theInputNumber.Call();
            }
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (item != null)
            {
                //Debug.Log(itemImage.sprite.name);
                //DragSlot.instance.gameObject.SetActive(true);
                DragSlot.instance.DragSetImage(itemImage);
                DragSlot.instance.dragSlot = this;
                DragSlot.instance.transform.position = eventData.position;
            }
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (item != null)
            {
                DragSlot.instance.transform.position = eventData.position;
            }
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            //DragSlot.instance.transform.position = Vector3.zero;
            //DragSlot.instance.gameObject.SetActive(false);
            
            DragSlot.instance.SetColor(0);
            DragSlot.instance.dragSlot = null;
            
        }
        public void OnDrop(PointerEventData eventData)
        {
            if (DragSlot.instance.dragSlot != null)
            {
                ChangeSlot();
            }
        }

        private void ChangeSlot()
        {
            Item _tempItem = item;
            int _tempItemCount = itemCount;
            
            AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

            if (_tempItem != null)
            {
                DragSlot.instance.dragSlot.AddItem(_tempItem,_tempItemCount);
            }
            else
            {
                DragSlot.instance.dragSlot.ClearSlot();
            }
        }

        // 아이템 이미지의 투명도 조절
        private void SetColor(float _alpha)
        {
            Color color = itemImage.color;
            color.a = _alpha;
            itemImage.color = color;
        }
    
        // 인벤토리에 새로운 아이템 슬롯 추가
        public void AddItem(Item _item, int _count = 1)
        {
            item = _item;
            itemCount = _count;
            itemImage.sprite = item.itemImage;
    
            if(item.itemType != Item.ItemType.Equipment)
            {
                go_CountImage.SetActive(true);
                text_Count.text = itemCount.ToString();
            }
            else
            {
                text_Count.text = "0";
                go_CountImage.SetActive(false);
            }
    
            SetColor(1);
        }
    
        // 해당 슬롯의 아이템 갯수 업데이트
        public void SetSlotCount(int _count)
        {
            itemCount += _count;
            text_Count.text = itemCount.ToString();
    
            if (itemCount <= 0)
                ClearSlot();
        }
        public int GetSlotItemCount()
        {
            return itemCount;
        }

        // 해당 슬롯 하나 삭제
        private void ClearSlot()
        {
            item = null;
            itemCount = 0;
            itemImage.sprite = null;
            SetColor(0);
    
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if ((eventData.button == PointerEventData.InputButton.Left)&&(item != null))
            {
                
                Debug.Log(item.itemName + " 을 선택했습니다.");
                //Debug.Log(itemImage.sprite.name);
                //DragSlot.instance.SetColor(1);
                SelectedSlot.instance.selectSlot = this;
                SelectedSlot.instance.SelSetImage(itemImage);
                //SelectedSlot.instance.transform.position = eventData.position;
                FindObjectOfType<Inventory>().SetSelectedSlot(this);                
            }
            
            else if(eventData.button == PointerEventData.InputButton.Right)
            {
                if (item != null)
                {
                    if(item.itemType == Item.ItemType.Equipment)
                    {
                        Debug.Log(item.itemName + " 을 장착했습니다.");
                        // 장착
                        //StartCoroutine(theWeaponManager.ChangeWeaponCoroutine(item.weaponType, item.itemName));
                    }
                    else
                    {
                        // 소비
                        Debug.Log(item.itemName + " 을 사용했습니다.");
                        SetSlotCount(-1);
                    }
                }
            }
        }
}
