using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests;
    public static QuestManager instance;
    private HashSet<Quest> completedQuests;
    public Inventory inventory;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.LogWarning("Instance already exists, destroying object!");
            Destroy(this);
        }
    }
    private void Start()
    {
        completedQuests = new HashSet<Quest>();
        inventory.OnItemAdded += UpdateQuestsOnItemAdded;
        inventory.OnItemRemoved += UpdateQuestsOnItemRemoved;
    }

    private void UpdateQuestsOnItemAdded(Item item, int amount)
    {
        // Loop through the quests to update them.
        foreach (Quest quest in quests)
        {
            if (quest.requiredItem == item && quest.questState == QuestState.QuestInProgress)
            {
                UpdateQuestProgress(quest, amount);
            }
        }
    }
    private void UpdateQuestsOnItemRemoved(Item item, int amount)
    {
        // Loop through the quests to update them.
        foreach (Quest quest in quests)
        {
            if (quest.requiredItem == item && quest.questState == QuestState.QuestCompletable)
            {
                quest.questState = QuestState.QuestInProgress;
            }
        }
    }

    public void UpdateQuestProgress(Quest quest, int progress)
    {
        if (!quest.isCompleted)
        {
            quest.currentProgress = Mathf.Clamp(quest.currentProgress + progress, 0, quest.targetProgress);

            if (quest.currentProgress >= quest.targetProgress)
            {
                quest.questState = QuestState.QuestCompletable;
            }
        }
    }

    public void CompleteQuest(Quest quest)
    {
        if (quest.questState == QuestState.QuestCompletable)
        {
            quest.isCompleted = true;
            inventory.RemoveItem(quest.requiredItem, quest.targetProgress);  // Assume that 1 is the required amount of the item

            foreach (Reward reward in quest.rewards)
            {
                GrantReward(reward);
            }
            completedQuests.Add(quest);
            quest.questState = QuestState.QuestCompleted;
        }
        else
        {
            Debug.LogWarning("퀘스트를 완료할 수 없습니다. 진행 상황이 목표에 도달하지 않았습니다.");
        }
    }
    public bool IsQuestCompleted(Quest quest)
    {
        return completedQuests.Contains(quest);
    }
    private void GrantReward(Reward reward)
    {
        // Grant the reward based on its type
    }
    public void CheckAndUpdateQuest(Quest quest, Item item, int requiredAmount)
    {
        if (quest.questState == QuestState.QuestInProgress)
        {
            if (inventory.HasEnoughItems(item, requiredAmount))
            {
                quest.questState = QuestState.QuestCompletable;
            }
        }
    }
    public void AddQuest(Quest newQuest)
    {
        if (!quests.Contains(newQuest))
        {
            quests.Add(newQuest);
        }
        else
        {
            Debug.LogWarning("이미 퀘스트 목록에 있는 퀘스트입니다: " + newQuest.name);
        }
    }

}