using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchOnOff : MonoBehaviour
{
    public bool touchlight;
    public int touchnum;

    PatternController bosspattern;

    void Start()
    {
        bosspattern = GameObject.Find("Boss").GetComponent<PatternController>(); //보스 오브젝트의 패턴 함수 가져오기

    }

    void Update()
    {
        if (bosspattern.bossspawn)
        {
            //touches[bosspattern.target] 점화 애니메이션 구현 부분
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.F))
        {
            if (!touchlight)
            {
                touchlight = true;
                bosspattern.TouchOnOffControl(touchnum, touchlight);
            }
        }else if(other.gameObject.tag == "Boss")
        {
            if (touchlight)
            {
                touchlight = false;
                bosspattern.TouchOnOffControl(touchnum, touchlight);
                bosspattern.RunBoss();
            }
        }
    }
}
