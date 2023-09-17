using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//다이어로그에서 정수형 변수를 받아와 그에 맞는 배열을 1(활성화)로 바꿔주는 형식
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

    
    //대화상태 카메라 제어
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
    //npc camera 초기화
    private void InitialisationNPC()
    {
        npc_cameras[0] = player_camera;
        npc_cameras[1] = rinda_camera;
    }
    //production camera 초기화
    private void InitialisationProduction()
    {

    }
}
