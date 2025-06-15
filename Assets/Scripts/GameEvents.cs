using System;
using System.Collections.Generic;

public static class GameEvents
{
    public static Action<string, int> OnGoalProgress;
    // e.g. id = "enemy_zombie", amount = 1

    public static void GoalProgress(string id, int amount = 1)
    {
        OnGoalProgress?.Invoke(id, amount);
    }
}