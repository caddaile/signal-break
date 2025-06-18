using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(menuName = "Quest/Quest Chain")]
public class QuestChain : ScriptableObject
{
    public List<Quest> quests;


    public Quest GetQuestAt(int index) => index >= 0 && index < quests.Count ? quests[index] : null;

    [Serializable]
    public class Quest
    {
        public string title;
        public List<QuestGoal> goals;

        public bool IsComplete => goals != null && goals.All(g => g.IsComplete);
    }

    [Serializable]
    public class QuestGoal
    {
        public string title;
        public string relay;
        public QuestTargetType targetType;
        public int requiredAmount = 1;
        [HideInInspector] public int currentAmount = 0;
        public bool isHidden = false;

        public bool IsComplete => currentAmount >= requiredAmount;

        public bool RegisterProgress(QuestTargetType id, int amount = 1)
        {
            if (id == targetType && !IsComplete)
            {
                currentAmount += amount;
            }
            if (id == targetType && IsComplete)
            {
                GameEvents.GoalCompleted(relay);
            }
            return IsComplete;
        }

        public void Reveal()
        {
            isHidden = false;
        }
    }
}