using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    // ����â
    [SerializeField] private GameObject settingPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // �г� on �Լ� (��Ϲ���)
    public void settingPanelOn()
    {
        settingPanel.gameObject.SetActive(true);
    }

    // �г� off �Լ� (�г� �� resume ��ư)
    public void settingPanelOff()
    {
        settingPanel.gameObject.SetActive(false);
    }

    // ���� ���� (���� �� ����)

    // ��� ����
    // ȿ���� ����
}
