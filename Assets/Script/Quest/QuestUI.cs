using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public QuestManager questManager;
    public GameObject questItemPrefab;
    public GameObject completedQuestItemPrefab;
    public Button inProgressTabButton;
    public Button completedTabButton;
    public Transform activeQuestListContent;
    public Transform completedQuestListContent;
    public TextMeshProUGUI questDetailsTitle;
    public TextMeshProUGUI questDetailsDescription;
    //public Transform dialogueLogContent;
    //public Transform questDetailsRewardList;
    
    public GameObject rewardItemPrefab;
    public Slider questDetailsProgressSlider;

    void OnEnable()
    {
        // Subscribe to quest progress change events
        foreach (Quest quest in questManager.quests)
        {
            quest.OnQuestProgressChanged += UpdateQuestList;
        }
        PopulateQuestList();
        OnInProgressTabSelected();
    }

    void OnDisable()
    {
        // Unsubscribe from quest progress change events
        foreach (Quest quest in questManager.quests)
        {
            quest.OnQuestProgressChanged -= UpdateQuestList;
            
        }
    }

    public void UpdateQuestList()
    {
        PopulateQuestList();
    }
    public void PopulateQuestList()
    {
        // Remove existing quest items from active and completed lists
        foreach (Transform child in activeQuestListContent)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in completedQuestListContent)
        {
            Destroy(child.gameObject);
        }
        
        // Populate the quest lists
        foreach (Quest quest in questManager.quests)
        {
            if (quest.isCompleted)
            {
                GameObject questItem = Instantiate(completedQuestItemPrefab, completedQuestListContent);
                questItem.GetComponent<QuestItem>().Initialize(quest, this);
            }
            else
            {
                GameObject questItem = Instantiate(questItemPrefab, activeQuestListContent);
                questItem.GetComponent<QuestItem>().Initialize(quest, this);
            }
        }
    }
    
    public void DisplayQuestDetails(Quest quest)
    {
        questDetailsTitle.text = quest.name;
        questDetailsDescription.text = quest.description;
        questDetailsProgressSlider.value = (float)quest.currentProgress / quest.targetProgress;
        quest.isNew = false; // Remove the "new" badge when displaying quest details
    }
    public void OnInProgressTabSelected()
    {
        inProgressTabButton.interactable = false;
        completedTabButton.interactable = true;

        activeQuestListContent.gameObject.SetActive(true);
        completedQuestListContent.gameObject.SetActive(false);
    }

    public void OnCompletedTabSelected()
    {
        inProgressTabButton.interactable = true;
        completedTabButton.interactable = false;

        activeQuestListContent.gameObject.SetActive(false);
        completedQuestListContent.gameObject.SetActive(true);
    }

}
