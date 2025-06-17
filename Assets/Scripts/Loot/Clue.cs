public class Clue : LootBase
{
    public override QuestTargetType TargetType => QuestTargetType.FindClue;

    public override void OnInteract()
    {
        GameEvents.GoalProgress(TargetType, 1);
        base.OnInteract();
    }
}