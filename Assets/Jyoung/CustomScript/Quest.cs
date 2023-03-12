using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public string title;
    public string description;
    public List<string> objectives;
    public bool isActive;
    public bool isCompleted;
    public bool isFailed;
    public int rewardGold;
    public int rewardXP;
}
