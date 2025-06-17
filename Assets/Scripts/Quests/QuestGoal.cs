using System;



[Serializable]
public class QuestGoal
{

    public string description;
    public QuestTargetType targetID;
    public int requiredAmount = 1;
    public int currentAmount = 0;
    public bool isHidden = false;

    public bool IsComplete => currentAmount >= requiredAmount;

    // Updates progress, returns true if it's finished
    public bool RegisterProgress(QuestTargetType id, int amount = 1)
    {
        if (id == targetID && !IsComplete)
        {
            currentAmount += amount;
        }

        return IsComplete;
    }

    public void Reveal()
    {
        isHidden = false;
    }
}