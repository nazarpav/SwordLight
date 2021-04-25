using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWindow : MonoBehaviour
{
    public string ShowWindowTrigger;
    public string HideWindowTrigger;
    public Animator Animator;
    public void Show()
    {
        Animator.SetTrigger(ShowWindowTrigger);
    }
    public void Hide()
    {
        Animator.SetTrigger(HideWindowTrigger);
    }
}
