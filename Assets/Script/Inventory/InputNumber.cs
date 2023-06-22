using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class InputNumber : MonoBehaviour
{
    private bool activated = false;

    [SerializeField]
    private TextMeshProUGUI text_Preview;  
    [SerializeField]
    private TextMeshProUGUI text_Input;  
    [SerializeField]
    private TMP_InputField if_text;

    [SerializeField]
    private GameObject go_Base;

    [SerializeField]
    private Player thePlayer;
    private Slot currentSlot;
    
    //public DropItemController dropItemController;
    public void Call()
    {
        go_Base.SetActive(true);
        activated = true;
        if_text.text = "";
        text_Preview.text = SelectedSlot.instance.selectSlot.itemCount.ToString();
    }
    public void Cancel()
    {
        Debug.Log(if_text.text);
        activated = false;
        SelectedSlot.instance.SetColor(0);
        go_Base.SetActive(false);
        SelectedSlot.instance.selectSlot = null;
    }

    public void OK()
    {
        //SelectedSlot.instance.SetColor(0);
        //Debug.Log("Input text : " + amount);
        //Debug.Log("Check result : "+ CheckNumber(amount));
        int num = 0;
        if (CheckNumber(if_text.text))
        {
            num = int.Parse(if_text.text);
        }
        else
        {
            num = SelectedSlot.instance.selectSlot.itemCount;
        }
        DropItems(num);
    }
    public void Call(Slot selectedSlot)
    {
        currentSlot = selectedSlot;
        go_Base.SetActive(true);
        activated = true;
        if_text.text = "";
        text_Preview.text = currentSlot.itemCount.ToString();
    }
    public void DropItems(int _num)
    {
        Debug.Log(SelectedSlot.instance.selectSlot.item.itemPrefab.name);
        for (int i = 0; i < _num; i++)
        {
            SelectedSlot.instance.selectSlot.SetSlotCount(-1);
        }

        SelectedSlot.instance.selectSlot = null;
        go_Base.SetActive(false);
        activated = false;
    }
    private bool CheckNumber(string _argString)
    {
        int result;
        return int.TryParse(_argString, out result);
    }
    void Update()
    {
        //Debug.Log(int.Parse(if_text.text));
        if (activated)
        {
            if (Input.GetKeyDown(KeyCode.Return))
                OK();
            else if (Input.GetKeyDown(KeyCode.Escape))
                Cancel();
        }
    }

}
