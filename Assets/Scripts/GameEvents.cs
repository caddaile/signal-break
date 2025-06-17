using System;
using System.Collections.Generic;

public enum QuestTargetType
{
    KillInfected,
    SurviveWave,
    FindClue
}
public static class GameEvents
{
    public static Action<QuestTargetType, int> OnGoalProgress;

    public static void GoalProgress(QuestTargetType id, int amount = 1)
    {
        OnGoalProgress?.Invoke(id, amount);
    }
}