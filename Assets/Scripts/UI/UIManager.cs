using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Canvases")]
    public GameObject hudCanvas;
    public GameObject menuCanvas;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ShowHUD()
    {
        hudCanvas.SetActive(true);
        menuCanvas.SetActive(false);
    }

    public void ShowMenu()
    {
        hudCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }
}