using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>();
    public void AddQuest(Quest quest)
    {
        quests.Add(quest);
    }

    public void RemoveQuest(Quest quest)
    {
        quests.Remove(quest);
    }

    public void UpdateQuestObjective(Quest quest, string objective)
    {
        if (quest.objectives.Contains(objective))
        {
            quest.objectives.Remove(objective);
            if (quest.objectives.Count == 0)
            {
                quest.isCompleted = true;
                // Handle quest completion event
            }
        }
    }
}
