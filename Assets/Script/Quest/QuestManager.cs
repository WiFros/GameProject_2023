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
    }
    
    public void UpdateQuestProgress(Quest quest, int progress)
    {
        if (!quest.isCompleted)
        {
            quest.currentProgress = Mathf.Clamp(quest.currentProgress + progress, 0, quest.targetProgress);

            if (quest.currentProgress >= quest.targetProgress)
            {
                CompleteQuest(quest);
            }
        }
    }

    private void CompleteQuest(Quest quest)
    {
        quest.isCompleted = true;

        foreach (Reward reward in quest.rewards)
        {
            GrantReward(reward);
        }
        completedQuests.Add(quest);
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