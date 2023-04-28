using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestItem : MonoBehaviour,IPointerClickHandler
{
    public Quest quest;
    public QuestUI questUI;
    public TextMeshProUGUI questName;
    public TextMeshProUGUI questStatus;
    public TextMeshProUGUI progressSliderText;
    public GameObject newQuestBadge;
    public Slider progressSlider;
    
    public void Initialize(Quest quest, QuestUI questUI)
    {
        this.quest = quest;
        this.questUI = questUI;
        questName.text = quest.name;
        questStatus.text = quest.description;
        if (!this.quest.isCompleted)
        {
            progressSlider.maxValue = quest.targetProgress;
            progressSlider.value = quest.currentProgress;
            progressSliderText.text = quest.currentProgress.ToString() + '/' + quest.targetProgress.ToString();
            newQuestBadge.SetActive(quest.isNew);
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        questUI.DisplayQuestDetails(quest);
        if (quest.isNew)
        {
            quest.isNew = false;
            newQuestBadge.SetActive(false);
        }  
    }
}