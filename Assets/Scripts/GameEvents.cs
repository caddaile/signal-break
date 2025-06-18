using System;

public enum QuestTargetType
{
    Slain,
    Survive,
    Collect,
    Visit
}
public static class GameEvents
{
    public static Action<QuestTargetType, int> OnGoalProgress;
    public static Action<string> OnGoalCompleted;

    public static void GoalProgress(QuestTargetType id, int amount = 1)
    {
        OnGoalProgress?.Invoke(id, amount);
    }
    public static void GoalCompleted(string id)
    {
        OnGoalCompleted?.Invoke(id);
    }
}