using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests;
    
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
    }

    private void GrantReward(Reward reward)
    {
        // Grant the reward based on its type
    }
}