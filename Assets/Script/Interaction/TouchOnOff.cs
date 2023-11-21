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
        bosspattern = GameObject.Find("Boss").GetComponent<PatternController>(); //���� ������Ʈ�� ���� �Լ� ��������

    }

    void Update()
    {
        if (bosspattern.bossspawn)
        {
            //touches[bosspattern.target] ��ȭ �ִϸ��̼� ���� �κ�
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.F))
        {
            if (!touchlight)
            {
                touchlight = true;
                bosspattern.SpawnBoss(touchnum);
            }
        }else if(other.gameObject.tag == "Boss")
        {
            if (touchlight)
            {
                touchlight = false;
                bosspattern.RunBoss();
            }
        }
    }
}
