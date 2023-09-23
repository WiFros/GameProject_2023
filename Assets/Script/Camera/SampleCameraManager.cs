using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���̾�α׿��� ������ ������ �޾ƿ� �׿� �´� �迭�� 1(Ȱ��ȭ)�� �ٲ��ִ� ����
public class SampleCameraManager : MonoBehaviour
{
    public Camera main_camera;
    public Camera player_camera;
    public Camera rinda_camera;
    public Camera[] npc_cameras = new Camera[10];
    public Camera[] production_cameras = new Camera[100];
    
    bool mainC = true;

    void Start()
    {
        InitialisationNPC();
        InitialisationProduction();
        main_camera.enabled = true;
        for (int i = 0; i < npc_cameras.GetLength(0) - 1; i++)
        {
            if (npc_cameras[i] != null)
            {
                npc_cameras[i].enabled = false;
            }
        }
        for (int i = 0; i < production_cameras.GetLength(0) - 1; i++)
        {
            if (production_cameras[i] != null)
            {
                production_cameras[i].enabled = false;
            }
        }
    }

    void Update()
    {

    }

    public void CameraChaingeToMain()
    {
        main_camera.enabled = true;
        for (int i = 0; i < npc_cameras.GetLength(0) - 1; i++)
        {
            if(npc_cameras[i] != null)
            {
                npc_cameras[i].enabled = false;
            }
        }
        for (int i = 0; i < production_cameras.GetLength(0) - 1; i++)
        {
            if (production_cameras[i] != null)
            {
                production_cameras[i].enabled = false;
            }
        }
    }

    
    //��ȭ���� ī�޶� ����
    public void CameraChaingeToCharacter(int cn)
    {
        main_camera.enabled = false;
        for (int i = 0; i < npc_cameras.GetLength(0) - 1; i++)
        {
            if (npc_cameras[i] != null)
            {
                npc_cameras[i].enabled = false;
            }
        }
        npc_cameras[cn].enabled = true;
    }
    //npc camera �ʱ�ȭ
    private void InitialisationNPC()
    {
        npc_cameras[0] = player_camera;
        npc_cameras[1] = rinda_camera;
    }
    //production camera �ʱ�ȭ
    private void InitialisationProduction()
    {

    }
}
