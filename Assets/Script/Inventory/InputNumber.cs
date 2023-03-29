using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
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
    private CharacterController thePlayer;
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
        activated = false;
        SelectedSlot.instance.SetColor(0);
        go_Base.SetActive(false);
        SelectedSlot.instance.selectSlot = null;
    }

    public void OK()
    {
        SelectedSlot.instance.SetColor(0);
        Debug.Log("Inputtext : " + text_Input.text);
        Debug.Log("Check result : "+ CheckNumber(text_Input.text));
        //Debug.Log(text_Input.text);
        int num;
        if (text_Input.text != "")
        {
            if (int.TryParse(text_Input.text, out num))
            {
                Debug.Log(int.TryParse(text_Input.text, out num));
                num = int.Parse(text_Input.text);
                if (num > SelectedSlot.instance.selectSlot.itemCount)
                    num = SelectedSlot.instance.selectSlot.itemCount;
            }
            else
                num = 1;
        }
        else
        {
            num = int.Parse(text_Preview.text);
        }
        go_Base.SetActive(true);
        gameObject.SetActive(true);
        /*
        dropItemController.StartCoroutine(dropItemController.DropItem(
            DragSlot.instance.dragSlot.item.itemPrefab,
            thePlayer.transform,
            num,
            DragSlot.instance.dragSlot));
            */
        StartCoroutine(DropItemCorountine(num));
        //StartCoroutine("DropItemCorountine", num);
    }

    IEnumerator DropItemCorountine(int _num)
    {
        Debug.Log(SelectedSlot.instance.selectSlot.item.itemPrefab.name);
        for (int i = 0; i < _num; i++)
        {
            if (SelectedSlot.instance.selectSlot.item.itemPrefab != null)
            {
                Instantiate(SelectedSlot.instance.selectSlot.item.itemPrefab,
                    thePlayer.transform.position+Vector3.up*0.5f,
                    Quaternion.identity);
            }

            SelectedSlot.instance.selectSlot.SetSlotCount(-1);
            yield return new WaitForSeconds(0.05f);
        }
        
        SelectedSlot.instance.selectSlot = null;
        go_Base.SetActive(false);
        activated = false;
    }
    public void Call(Slot selectedSlot)
    {
        currentSlot = selectedSlot;
        go_Base.SetActive(true);
        activated = true;
        if_text.text = "";
        text_Preview.text = currentSlot.itemCount.ToString();
    }
    private bool CheckNumber(string _argString)
    {
        return true;
    }
    void Update()
    {
        if (activated)
        {
            if (Input.GetKeyDown(KeyCode.Return))
                OK();
            else if (Input.GetKeyDown(KeyCode.Escape))
                Cancel();
        }
    }
}
