using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum NPCState
{
    Normal,
    QuestAvailable,
    QuestInProgress,
    QuestCompleted
}
public class NPC : MonoBehaviour
{
    public string npcName;
    public NPCState currentState;
    public Dialogue normalDialogue;
    public Dialogue questAvailableDialogue;
    public Dialogue questInProgressDialogue;
    public Dialogue questCompletedDialogue;
    public Dialogue specialDialogue;

    public GameObject questIcon;
    public Sprite exclamationMark;
    public Sprite questionMark;
    public Sprite inProgressMark;

    private DialogueTrigger dialogueTrigger;
    private Image questIconImage;

    private void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
        questIconImage = questIcon.GetComponent<Image>();

        UpdateNPCState();
    }

    public void UpdateNPCState()
    {
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
                questIcon.SetActive(true);
                questIconImage.sprite = questionMark;
                dialogueTrigger.dialogue = questCompletedDialogue;
                break;
            default:
                questIcon.SetActive(false);
                dialogueTrigger.dialogue = specialDialogue;
                break;
        }
    }

    public void OnInteract()
    {
        dialogueTrigger.TriggerDialogue();
    }
}
