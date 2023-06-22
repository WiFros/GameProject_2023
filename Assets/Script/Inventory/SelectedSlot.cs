using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedSlot : MonoBehaviour
{
    static public SelectedSlot instance;
    public Slot selectSlot;

    [SerializeField] private Image imageItem;
    [SerializeField] private TextMeshProUGUI text_Count;
    [SerializeField] private TextMeshProUGUI text_Name;
    [SerializeField] private TextMeshProUGUI text_Description;
    
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //instance.SetColor(0);
    }

    public void SelSetImage(Image _image)
    {
        imageItem.sprite = _image.sprite;
        text_Count.text = selectSlot.itemCount.ToString();
        text_Name.text = selectSlot.item.itemName;
        text_Description.text = selectSlot.item.description;
        
        SetColor(1);
    }

    public void SetColor(float _alpha)
    {
        Color color = imageItem.color;
        color.a = _alpha;
        imageItem.color = color;
    }
}
