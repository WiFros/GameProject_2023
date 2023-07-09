using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject inventoryCanvas;
    public GameObject questCanvas;
    public GameObject playerUI;
    private bool isActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isActive = !isActive;
            inventoryCanvas.SetActive(isActive);
            playerUI.SetActive(!isActive);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isActive = !isActive;
            questCanvas.SetActive(isActive);
            playerUI.SetActive(!isActive);
        }
    }
    public bool CanStartQuest(Quest quest)
    {
        foreach (string prerequisiteId in quest.prerequisiteQuests)
        {
            Quest prerequisiteQuest = GetQuestById(prerequisiteId);

            if (prerequisiteQuest == null || !prerequisiteQuest.isCompleted)
            {
                return false;
            }
        }

        return true;
    }
    // GetQuestById 메소드는 아이디로 퀘스트를 찾아 반환하는 메소드입니다.
    
    public Quest GetQuestById(string id)
    {
        return null;
        // 모든 퀘스트를 검사하여 id와 일치하는 퀘스트를 찾습니다.
        // 해당 퀘스트를 반환하거나, 없으면 null을 반환합니다.
    }
}
