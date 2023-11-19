using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchOnOff : MonoBehaviour
{
    public bool[] touchlight = new bool[6];
    public GameObject[] touches = new GameObject[6];

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
            for (int i = 0; i < touches.Length; i++)
            {
                if(touches[i] == gameObject)
                {
                    touchlight[i] = true;
                    bosspattern.SpawnBoss(touches[i]);
                    break;
                }
            }
        }else if(other.gameObject.tag == "Boss")
        {
            for (int i = 0; i < touches.Length; i++)
            {
                if (touches[i] == gameObject)
                {
                    touchlight[i] = false;
                    bosspattern.RunBoss();
                    break;
                }
            }
        }
    }
}
