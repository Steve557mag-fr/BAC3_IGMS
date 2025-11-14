using System;
using TMPro;
using UnityEngine;

public class UIDialog : MonoBehaviour
{

    [SerializeField] GameObject dialogPanel;
    [SerializeField] TextMeshProUGUI dialogText;

    [SerializeField] LeanTweenType textEaseType;
    [SerializeField] float textDuration = 1;

    public void UploadSeq(string message, Action<string> onUpdate=null, Action onFinished=null)
    {
        dialogPanel.SetActive(true);

        LeanTween.value(0,1, textDuration).setEase(textEaseType).setOnUpdate((float t) =>
        {
            var displayedMessage= message.Substring(0, Mathf.CeilToInt(
                Mathf.Lerp(0, message.Length, t)
            ));

            dialogText.text = displayedMessage;
            onUpdate?.Invoke(displayedMessage);

        }).setOnComplete(() =>
        {
            Debug.Log("FINNNN");
            onFinished?.Invoke();
        });

    }

    public void SetRich(CharacterRich? characterRich)
    {
        dialogText.color = characterRich.HasValue ? Color.white : characterRich.Value.color;

    }

    public void CloseSeq()
    {
        dialogPanel.SetActive(false);
    }


}
