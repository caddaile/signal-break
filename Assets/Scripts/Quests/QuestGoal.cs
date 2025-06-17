using System;
using UnityEngine;

[Serializable]
public class QuestGoal
{
    public string goalId;
    public string description;
    public QuestTargetType targetType;
    public int requiredAmount = 1;
    [HideInInspector] public int currentAmount = 0;
    public bool isHidden = false;

    public bool IsComplete => currentAmount >= requiredAmount;


    // Updates progress, returns true if it's finished
    public bool RegisterProgress(QuestTargetType id, int amount = 1)
    {
        if (id == targetType && !IsComplete)
        {
            currentAmount += amount;
        }
        if (id == targetType && IsComplete)
        {
            GameEvents.GoalCompleted(goalId);
        }
        return IsComplete;
    }

    public void Reveal()
    {
        isHidden = false;
    }
}