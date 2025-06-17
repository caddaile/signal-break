using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(menuName = "Game/Quest")]
public class Quest : ScriptableObject
{
    public string title;
    [TextArea] public string description;

    public List<QuestGoal> goals;

    public bool IsComplete => goals != null && goals.All(g => g.IsComplete);
}