using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    // 설정창
    [SerializeField] private GameObject settingPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 패널 on 함수 (톱니바퀴)
    public void settingPanelOn()
    {
        settingPanel.gameObject.SetActive(true);
    }

    // 패널 off 함수 (패널 내 resume 버튼)
    public void settingPanelOff()
    {
        settingPanel.gameObject.SetActive(false);
    }

    // 게임 종료 (저장 후 종료)

    // 브금 볼륨
    // 효과음 볼륨
}
