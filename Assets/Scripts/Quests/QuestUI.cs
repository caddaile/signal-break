using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private QuestManager questManager;
    [SerializeField] private TextMeshProUGUI questTitleText;
    [SerializeField] private Transform goalsContainer;
    [SerializeField] private GameObject goalEntryPrefab;

    private List<GameObject> currentEntries = new();

    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        var quest = questManager.ActiveQuest;
        if (quest == null)
        {
            questTitleText.text = "All quests complete!";
            ClearGoals();
            return;
        }

        questTitleText.text = quest.title;

        ClearGoals();
        foreach (var goal in quest.goals)
        {
            if (goal.isHidden) continue;

            var entry = Instantiate(goalEntryPrefab, goalsContainer);
            var text = entry.GetComponentInChildren<TextMeshProUGUI>();
            text.text = $"{goal.title} ({goal.currentAmount}/{goal.requiredAmount})";
            currentEntries.Add(entry);
        }
    }

    private void ClearGoals()
    {
        foreach (var go in currentEntries) Destroy(go);
        currentEntries.Clear();
    }

    void OnEnable()
    {
        QuestManager.OnQuestUpdated += UpdateUI;
    }

    void OnDisable()
    {
        QuestManager.OnQuestUpdated -= UpdateUI;
    }
}