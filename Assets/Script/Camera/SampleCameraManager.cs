using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��� ������ ī�޶�� �迭�� ������ ��
//ī�޶� �迭�� ����: npc ī�޶�, ������ ���� ī�޶�, �� �� ���� ī�޶�
//npcī�޶�� npc�̸��� �ٽ� ������ �ٲپ ����ġ���� �ƴ� for������ �迭 �˻������ �ۼ��� ����
public class SampleCameraManager : MonoBehaviour
{
    public Camera main_camera;
    //public Camera first_camera;
    //public Camera second_camera;
    public Camera player_camera;
    public Camera npc_camera;

    bool mainC = true;

    void Start()
    {
        main_camera.enabled = true;
        player_camera.enabled = false;
        npc_camera.enabled = false;
        //first_camera.enabled = false;
        //second_camera.enabled = false;
        //Invoke("FirstCamera", 1.5f);
        //Invoke("SecondCamera", 3f);
    }

    void Update()
    {

    }
    /*void FirstCamera()
    {
        main_camera.enabled = false;
        first_camera.enabled = true;
        second_camera.enabled = false;
    }
    void SecondCamera()
    {
        main_camera.enabled = false;
        first_camera.enabled = false;
        second_camera.enabled = true;
    }*/

    public void CameraChaingeToMain()
    {
        npc_camera.enabled = false;
        main_camera.enabled = true;
    }

    
    //��ȭ���� ī�޶� ����
    public void CameraChaingeToPlayer()
    {
        main_camera.enabled = false;
        npc_camera.enabled = false;
        player_camera.enabled = true;
    }

    public void CameraChaingeToNPC(string name)
    {
        switch (name){
            case "����":
                main_camera.enabled = false;
                player_camera.enabled = false;
                npc_camera.enabled = true;
                break;
        }
    }
}
