using System;
using System.Collections.Generic;
using UnityEngine;

public enum QuestState
{
    NotAvailable,
    QuestAvailable,
    QuestInProgress,
    QuestCompletable,
    QuestCompleted
}

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Create new quest", order = 1)]
[System.Serializable]
public class Quest : ScriptableObject
{
    public string id;
    public string name;
    public string description;
    public string longDescription;
    public bool isNew;
    public bool isCompleted;
    public int targetProgress;
    public List<Reward> rewards;
    public List<string> prerequisiteQuests;
    public Dialogue questDialogue;
    public QuestState questState;
    public Item requiredItem;
    public event Action OnQuestProgressChanged;
    [HideInInspector]
    public List<Quest> prerequisiteQuestObjects; // 실제 Quest 참조를 위한 리스트

    public Quest()
    {
        id = Guid.NewGuid().ToString();
    }
    // 선행 퀘스트를 가져오기
    public void RetrievePrerequisiteQuests(List<Quest> allQuests)
    {
        prerequisiteQuestObjects = new List<Quest>();
        foreach (string questID in prerequisiteQuests)
        {
            Quest quest = allQuests.Find(q => q.id == questID);
            if (quest != null)
            {
                prerequisiteQuestObjects.Add(quest);
            }
        }
    }
    public bool ArePrerequisitesCompleted()
    {
        foreach (Quest quest in prerequisiteQuestObjects)
        {
            if (quest.questState != QuestState.QuestCompleted)
            {
                return false;
            }
        }
        return true;
    }
    private void OnValidate()
    {
        // If the ID is empty or not set, assign a new unique ID
        if (string.IsNullOrEmpty(id))
        {
            id = Guid.NewGuid().ToString();
        }
    }
    public int currentProgress
    {
        get => _currentProgress;
        set
        {
            _currentProgress = value;
            OnQuestProgressChanged?.Invoke();
        }
    }
    public int _currentProgress;
    
}