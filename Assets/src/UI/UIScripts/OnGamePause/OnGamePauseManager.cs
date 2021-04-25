using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnGamePauseManager : BaseWindow
{
    public Button ContinueButton;
    public Button NON_USE;
    public Button ExitToMainMenuButton;
    public Scrollbar MusicScrollBar;
    public Scrollbar SoundScrollBar;
    public Toggle AutoRetryToggle;
    public Toggle ProgressBarVisibleToggle;
    public Toggle AutoCheckpointsToggle;
    public string MainMenuSceneName;
    void Start()
    {
        ContinueButton?.onClick.AddListener(ContinueButtonOnClick);
        ExitToMainMenuButton?.onClick.AddListener(ExitToMainMenuButtonOnClick);
    }

    void ContinueButtonOnClick()
    {
        Hide();
    }
    void ExitToMainMenuButtonOnClick()
    {
        try
        {
            SceneManager.LoadScene(MainMenuSceneName);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }
}
