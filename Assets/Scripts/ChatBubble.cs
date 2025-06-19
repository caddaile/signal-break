using System.Collections;
using TMPro;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    Transform camTransform;

    void Start()
    {
        gameObject.SetActive(false);
        camTransform = Camera.main.transform;
    }

    void Update()
    {
        var camForward = camTransform.forward;

        if (camForward.sqrMagnitude > 0.001f)
        {
            var upwardTilt = Quaternion.Euler(0f, 0f, 0f); // Adjust tilt angle here
            transform.rotation = Quaternion.LookRotation(camForward) * upwardTilt;
        }
    }

    public void ShowMessage(string text)
    {
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