using System;
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
    public static bool GameIsPaused { get; set; }

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
        TogglePause();
        try
        {
            SceneManager.LoadScene(MainMenuSceneName);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }
    public static void TogglePause()
    {
        //if (GameIsPaused)
        //{
        //    GameIsPaused = false;
        //    Time.timeScale = 1.0f;
        //}
        //else
        //{
        //    GameIsPaused = true;
        //    Time.timeScale = 0.0f;
        //}
    }
}
