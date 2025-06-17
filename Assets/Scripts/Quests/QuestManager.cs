using UnityEngine;
using System;

public class QuestManager : MonoBehaviour
{
    public static event Action OnQuestUpdated;

    [SerializeField] private QuestChain questChain;
    private int currentQuestIndex = 0;
    private Quest activeQuest;

    public Quest ActiveQuest => activeQuest;

    void Start()
    {
        StartChain(questChain);
    }

    public void StartChain(QuestChain chain)
    {
        questChain = chain;
        currentQuestIndex = 0;
        activeQuest = CloneQuest(questChain.GetQuestAt(currentQuestIndex));
        OnQuestUpdated?.Invoke();
    }

    public void UpdateGoalProgress(string id, int amount = 1)
    {
        if (activeQuest == null) return;

        foreach (var goal in activeQuest.goals)
        {
            goal.RegisterProgress(id, amount);
        }

        if (activeQuest.IsComplete)
        {
            AdvanceToNextQuest();
        }

        OnQuestUpdated?.Invoke(); // trigger UI update
    }

    private void AdvanceToNextQuest()
    {
        currentQuestIndex++;
        var nextQuest = questChain.GetQuestAt(currentQuestIndex);
        if (nextQuest != null)
        {
            activeQuest = CloneQuest(nextQuest);

        }
        else
        {
            activeQuest = null; // no more quests
            Debug.Log("Quest chain completed!");
        }
    }

    void OnEnable()
    {
        GameEvents.OnGoalProgress += UpdateGoalProgress;
    }

    void OnDisable()
    {
        GameEvents.OnGoalProgress -= UpdateGoalProgress;
    }

    private Quest CloneQuest(Quest original)
    {
        var newQuest = ScriptableObject.CreateInstance<Quest>();
        newQuest.title = original.title;
        newQuest.description = original.description;
        newQuest.goals = new System.Collections.Generic.List<QuestGoal>();
        foreach (var goal in original.goals)
        {
            var newGoal = new QuestGoal
            {
                description = goal.description,
                targetID = goal.targetID,
                requiredAmount = goal.requiredAmount,
                currentAmount = 0
            };
            newQuest.goals.Add(newGoal);
        }
        return newQuest;
    }
}
