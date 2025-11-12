using System;
using UnityEngine;

public class UIGame : MonoBehaviour
{

    [SerializeField] CanvasGroup transitionGroup;
    [SerializeField] float transitionTime, transitionDelayTime;
    [SerializeField] LeanTweenType transitionType;

    public void MakeTransition(Action duringTransition = null)
    {
        transitionGroup.LeanAlpha(1, transitionTime).setEase(transitionType).setOnComplete(() =>
        {
            duringTransition?.Invoke();
            transitionGroup.LeanAlpha(0, transitionTime).setEase(transitionType).setDelay(transitionDelayTime);
        });
    }

}
