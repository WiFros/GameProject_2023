using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// NPC의 상태를 나타내는 열거형 변수
public enum NPCState
{
    Normal, // 일반 상태
    QuestAvailable, // 퀘스트를 제공할 수 있는 상태
    QuestInProgress, // 퀘스트가 진행 중인 상태
    QuestCompleted, // 퀘스트가 완료된 상태
    QuestCompletable // 퀘스트를 완료할 수 있는 상태
}

public class NPC : MonoBehaviour
{
    public string npcName; // NPC의 이름
    public NPCState currentState = NPCState.Normal; // 현재 NPC의 상태
    // 각 상태에 따른 대화
    public Dialogue normalDialogue;
    public Dialogue questAvailableDialogue;
    public Dialogue questInProgressDialogue;
    public Dialogue questCompletedDialogue;
    public Dialogue specialDialogue;
    public Dialogue questCompletableDialogue;

    // 퀘스트 아이콘과, 퀘스트 상태를 나타내는 스프라이트
    public GameObject questIcon;
    public Sprite exclamationMark;
    public Sprite questionMark;
    public Sprite inProgressMark;

    // NPC의 대화 트리거와 퀘스트 아이콘 이미지
    private DialogueTrigger dialogueTrigger;
    private Image questIconImage;
    
    // 특별한 대화를 위한 필요한 퀘스트 리스트
    public List<Quest> requiredQuestsForSpecialDialogue;
    public Dialogue dialogue;
    
    // 퀘스트 관리자와 NPC에 할당된 퀘스트, 인벤토리
    //public QuestManager questManager;
    public Quest assignedQuest;
    public Inventory inventory;

    // 매 업데이트마다 호출되는 함수
    void Update()
    {
        // NPC에 할당된 퀘스트가 있다면,
        // 퀘스트의 상태에 따라 NPC의 상태를 업데이트 합니다.
        if (assignedQuest != null)
        {
            switch (assignedQuest.questState)
            {
                case QuestState.QuestAvailable:
                    currentState = NPCState.QuestAvailable;
                    break;
                case QuestState.QuestInProgress:
                    currentState = NPCState.QuestInProgress;
                    break;
                case QuestState.QuestCompletable:
                    currentState = NPCState.QuestCompletable;
                    break;
                case QuestState.QuestCompleted:
                    currentState = NPCState.QuestCompleted;
                    break;
                default:
                    currentState = NPCState.Normal;
                    break;
            }
        }
        // NPC의 상태를 업데이트 합니다.
        UpdateNPCState();
    }

    // 초기화 함수
    private void Start()
    {
        // 대화 트리거와 퀘스트 아이콘 이미지 컴포넌트를 가져옵니다.
        dialogueTrigger = GetComponent<DialogueTrigger>();
        questIconImage = questIcon.GetComponent<Image>(); // NPC 상
        UpdateNPCState();
        CheckForSpecialDialogue();
    }

    public void UpdateNPCState()
    {
        //Debug.Log("Current NPC State: " + currentState);
        switch (currentState)
        {
            case NPCState.Normal:
                questIcon.SetActive(false);
                dialogueTrigger.dialogue = normalDialogue;
                break;
            case NPCState.QuestAvailable:
                questIcon.SetActive(true);
                questIconImage.sprite = exclamationMark;
                dialogueTrigger.dialogue = questAvailableDialogue;
                break;
            case NPCState.QuestInProgress:
                questIcon.SetActive(true);
                questIconImage.sprite = inProgressMark;
                dialogueTrigger.dialogue = questInProgressDialogue;
                break;
            case NPCState.QuestCompleted:
                questIcon.SetActive(false);
                dialogueTrigger.dialogue = questCompletedDialogue;
                break;
            case NPCState.QuestCompletable:
                questIcon.SetActive(true);
                questIconImage.sprite = questionMark;
                dialogueTrigger.dialogue = questCompletableDialogue;
                break;
            default:
                questIcon.SetActive(false);
                dialogueTrigger.dialogue = specialDialogue;
                break;
        }
    }

    private void CheckForSpecialDialogue()
    {
        if (requiredQuestsForSpecialDialogue.Count > 0)
        {
            bool allRequiredQuestsCompleted = true;

            foreach (Quest quest in requiredQuestsForSpecialDialogue)
            {
                if (!QuestManager.instance.IsQuestCompleted(quest))
                {
                    allRequiredQuestsCompleted = false;
                    break;
                }
            }

            if (allRequiredQuestsCompleted)
            {
                currentState = NPCState.QuestCompleted;
                dialogueTrigger.dialogue = specialDialogue;
            }
        }
    }
    public void InteractWithPlayer()
    {
        // 플레이어와의 상호작용 로직을 구현하세요.
        switch (currentState)
        {
            case NPCState.QuestAvailable:
                dialogueTrigger.TriggerDialogue();
                assignedQuest.questState = QuestState.QuestInProgress; // 퀘스트 상태를 진행 중으로 변경
                QuestManager.instance.AddQuest(assignedQuest);
                break;
            case NPCState.QuestInProgress:
                dialogueTrigger.TriggerDialogue();
                // 여기에서 퀘스트 진행을 확인하고 필요하다면 상태를 업데이트 합니다.
                if (inventory.CheckItem(assignedQuest.requiredItem, assignedQuest.targetProgress))
                {
                    assignedQuest.questState = QuestState.QuestCompletable;
                }
                break;
            case NPCState.QuestCompletable:
                dialogueTrigger.TriggerDialogue();
                // 퀘스트 완료
                //inventory.RemoveItem(assignedQuest.requiredItem, assignedQuest.targetProgress);
                QuestManager.instance.CompleteQuest(assignedQuest);
                break;
            case NPCState.QuestCompleted:
                dialogueTrigger.TriggerDialogue();
                // 퀘스트 완료 후의 로직을 구현하세요.
                break;
            default:
                // 일반 대화 로직을 구현하세요.
                break;
        }
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}
