using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RLGL : MonoBehaviour
{
    // 이 스크립트는 무궁화 꽃이 피었습니다 미니 게임을 위한 스크립트입니다.
    // NPC는 무 궁 화 꽃 이 피 었 습 니 다 로 이루어진 10개의 어절을 기점으로 뒤를 돌아볼지 안 돌아볼지 랜덤으로 판단합니다. 
    // NPC가 돌아보면 약간의 딜레이를 가지고 NPC의 상태를 '감지' 상태로 바꿉니다.
    // 플레이어는 어떤 움직임(메뉴 버튼 클릭도 포함) 이라고 하게 되면 '움직이는중' 상태로 바꾸게 되고, 이 상태에서 NPC의 상태도 '감지' 상태라면 플레이어가 패배하고 게임이 끝나게 됩니다.
    // 플레이어가 움직이지 않고 NPC가 '감지' 상태가 아니라면 게임은 계속 진행됩니다.
    // 플레이어가 NPC에게 다가가 상호작용 키를 누르면 게임은 종료되고 플레이어가 승리합니다. 
    
    public NPC npcController; // NPC의 동작을 제어할 컴포넌트
    public PlayerController playerController; // 플레이어의 동작을 제어할 컴포넌트
    public float detectionDelay = 1.0f; // NPC가 플레이어를 감지하는데 걸리는 시간

    private bool isGameActive = false;

}
