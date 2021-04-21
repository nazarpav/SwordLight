using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnGamePauseManager : MonoBehaviour
{
    public Button ContinueButton;
    public Button NON_USE;
    public Button ExitToMainMenuButton;
    public Scrollbar MusicScrollBar;
    public Scrollbar SoundScrollBar;
    public Toggle AutoRetryToggle;
    public Toggle ProgressBarVisibleToggle;
    public Toggle AutoCheckpointsToggle;
    public Animation ShowHideAnimation;
    public Animator ShowHideAnimator;
    void Start()
    {
        ContinueButton.onClick.AddListener(ContinueButtonOnClick);
    }
    void Show()
    {
        ShowHideAnimation.clip = ShowHideAnimation.GetClip("ShowWindow");
        ShowHideAnimation.Play();
    }
    void Hide()
    {
        ShowHideAnimation.clip = ShowHideAnimation.GetClip("HideWindow");
        ShowHideAnimation.Play();
    }
    void ContinueButtonOnClick()
    {
        Hide();
    }
}
