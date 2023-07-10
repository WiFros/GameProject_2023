using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutSceneManager : MonoBehaviour
{
    public static CutSceneManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += GetAllPlayableDirectorsInCurrentScene;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private PlayableDirector[] directors;

    public bool isRunning { get; private set; } = false;

    public void PlayCutScene(string cutSceneName)
    {
        foreach(PlayableDirector director in directors)
        {
            if(director.gameObject.name == cutSceneName)
            {
                director.Play();
                return;
            }
        }
        Debug.LogWarning(cutSceneName + "(을)를 찾을 수 없습니다.");
    }

    private void GetAllPlayableDirectorsInCurrentScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        directors = GameObject.FindObjectsOfType<PlayableDirector>();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            CutSceneManager.instance.PlayCutScene("LookPlayer");
            print("hi");
        }
    }
}
