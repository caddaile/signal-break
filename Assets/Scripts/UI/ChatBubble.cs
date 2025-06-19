using System.Collections;
using TMPro;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowMessage(string text)
    {
        if (text.Length == 0) return;

        _text.text = text;
        gameObject.SetActive(true);
        StartCoroutine(Hide());
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}