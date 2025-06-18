public class Clue : LootBase
{
    public override QuestTargetType TargetType => QuestTargetType.Collect;

    public override void OnInteract()
    {
        GameEvents.GoalProgress(TargetType, 1);
        base.OnInteract();
    }
}