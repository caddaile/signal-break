using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance { get; private set; }

    [SerializeField] private GameObject tooltipPrefab;
    private GameObject tooltipInstance;
    private Transform targetTransform;

    private readonly Vector3 offset = Vector3.up * 2f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (tooltipPrefab == null)
            Debug.LogError("Tooltip prefab not found");
    }

    private void LateUpdate()
    {
        if (tooltipInstance != null)
        {
            if (targetTransform == null)
            {
                HideTooltip();
                return;
            }

            tooltipInstance.transform.position = targetTransform.position + offset;
        }
    }

    public void ShowTooltip(Transform target)
    {
        if (tooltipInstance == null && tooltipPrefab != null)
        {
            tooltipInstance = Instantiate(tooltipPrefab);
        }

        targetTransform = target;

        if (tooltipInstance != null)
        {
            tooltipInstance.SetActive(true);
        }
    }

    public void HideTooltip()
    {
        if (tooltipInstance != null)
        {
            tooltipInstance.SetActive(false);
            targetTransform = null;
        }
    }
}