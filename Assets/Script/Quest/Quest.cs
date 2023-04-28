using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Create new quest", order = 1)]
[System.Serializable]
public class Quest : ScriptableObject
{
    public string id;
    public string name;
    public string description;
    public bool isNew;
    public bool isCompleted;
    public int targetProgress;
    public List<Reward> rewards;
    public List<string> prerequisiteQuests;
    public Dialogue questDialogue;
    public event Action OnQuestProgressChanged;
    public Quest()
    {
        id = Guid.NewGuid().ToString();
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