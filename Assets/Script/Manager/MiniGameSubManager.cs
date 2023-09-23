using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniGameSubManager : MonoBehaviour
{
    // 미니게임 시작
    public abstract void StartGame();

    // 미니게임 종료
    public abstract void EndGame();
}
