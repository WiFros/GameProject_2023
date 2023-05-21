using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public void Interact()
    {
        switch(quest.questState)
        {
            case QuestState.QuestAvailable:
                Debug.Log(quest.questDialogue); // replace with your dialogue display system
                break;
            case QuestState.QuestInProgress:
                Debug.Log("이미 진행중인 퀘스트 입니다."); // replace with your dialogue display system
                break;
            case QuestState.QuestCompletable:
                Debug.Log("퀘스트를 완료했습니다."); // replace with your dialogue display system
                quest.questState = QuestState.QuestCompleted;
                break;
            case QuestState.NotAvailable:
                Debug.Log("이미 완료된 퀘스트 입니다."); // replace with your dialogue display system
                break;
        }
    }
}
