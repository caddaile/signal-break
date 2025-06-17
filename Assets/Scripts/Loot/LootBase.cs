using UnityEngine;

public abstract class LootBase : MonoBehaviour
{
    public abstract QuestTargetType TargetType { get; }

    protected virtual void OnPlayerEnter()
    {
        // Show interaction prompt
        Debug.Log("Pick up");
    }

    protected virtual void OnPlayerExit()
    {
        // Hide interaction prompt
        Debug.Log("Exiting Pick up");
    }

    public virtual void OnInteract()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerExit();
        }
    }
}