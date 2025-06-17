using UnityEngine;
using UnityEngine.Events;

public class GoalRelay : MonoBehaviour
{
    [SerializeField] private string matchGoalId;
    [SerializeField] private UnityEvent onGoalCompleted;

    void OnEnable()
    {
        GameEvents.OnGoalCompleted += OnGoalComplete;
    }

    void OnDisable()
    {
        GameEvents.OnGoalCompleted -= OnGoalComplete;
    }

    void OnGoalComplete(string goalId)
    {
        Debug.Log("CHECK ID");
        if (goalId == matchGoalId)
        {
            Debug.Log("MATCH ID");
            onGoalCompleted?.Invoke();
        }
    }
}