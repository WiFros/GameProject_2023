using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedSlot : MonoBehaviour
{
    static public SelectedSlot instance;
    public Slot selectSlot;

    [SerializeField] private Image imageItem;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //instance.SetColor(0);
    }

    public void SelSetImage(Image _image)
    {
        Debug.Log("1111111");
        imageItem.sprite = _image.sprite;
        SetColor(1);
    }

    public void SetColor(float _alpha)
    {
        Color color = imageItem.color;
        color.a = _alpha;
        imageItem.color = color;
    }
}
