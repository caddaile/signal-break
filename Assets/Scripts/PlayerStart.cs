using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    void Start()
    {
        Instantiate(playerPrefab, transform.position, transform.rotation);
    }
}