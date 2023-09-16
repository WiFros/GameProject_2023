using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//모든 상태의 카메라는 배열로 정리할 것
//카메라 배열의 종류: npc 카메라, 아이템 연출 카메라, 그 외 연출 카메라
//npc카메라는 npc이름을 다시 정수로 바꾸어서 스위치문이 아닌 for문으로 배열 검사식으로 작성할 예정
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

    
    //대화상태 카메라 제어
    public void CameraChaingeToPlayer()
    {
        main_camera.enabled = false;
        npc_camera.enabled = false;
        player_camera.enabled = true;
    }

    public void CameraChaingeToNPC(string name)
    {
        switch (name){
            case "린다":
                main_camera.enabled = false;
                player_camera.enabled = false;
                npc_camera.enabled = true;
                break;
        }
    }
}
