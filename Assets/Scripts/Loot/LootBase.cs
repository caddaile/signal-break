using UnityEngine;

public abstract class LootBase : MonoBehaviour
{
    public abstract QuestTargetType TargetType { get; }

    [TextArea] public string displayText;

    void OnPlayerEnter()
    {
        TooltipManager.Instance?.ShowTooltip(transform);
    }

    void OnPlayerExit()
    {
        TooltipManager.Instance?.HideTooltip();
    }

    public virtual void OnInteract()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.transform.root.CompareTag("Player"))
        {
            OnPlayerEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.transform.root.CompareTag("Player"))
        {
            OnPlayerExit();
        }
    }
}