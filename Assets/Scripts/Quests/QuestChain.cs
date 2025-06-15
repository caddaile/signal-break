using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/Quest Chain")]
public class QuestChain : ScriptableObject {
    public string chainID;
    public List<Quest> quests;

    public int Length => quests.Count;

    public Quest GetQuestAt(int index) => index >= 0 && index < quests.Count ? quests[index] : null;
}