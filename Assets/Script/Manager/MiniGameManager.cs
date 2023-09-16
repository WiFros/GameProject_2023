using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager instance;
    // 현재 활성화된 미니게임
    private MiniGameSubManager currentMiniGame;
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 미니게임 시작
    public void StartMiniGame(MiniGameSubManager miniGame)
    {
        if(currentMiniGame != null)
        {
            currentMiniGame.EndGame(); // 현재 미니게임 종료
        }
        currentMiniGame = miniGame;
        currentMiniGame.StartGame();
    }

    // 미니게임 종료
    public void EndMiniGame()
    {
        if(currentMiniGame != null)
        {
            currentMiniGame.EndGame();
            currentMiniGame = null;
        }
    }
}
