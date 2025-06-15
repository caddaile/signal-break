using System;

[Serializable]
public class QuestGoal
{
    public string description;
    public string targetID;
    public int requiredAmount = 1;
    public int currentAmount = 0;

    public bool IsComplete => currentAmount >= requiredAmount;

    public void RegisterProgress(string id, int amount = 1)
    {
        if (id == targetID && !IsComplete)
        {
            currentAmount += amount;
        }
    }
}