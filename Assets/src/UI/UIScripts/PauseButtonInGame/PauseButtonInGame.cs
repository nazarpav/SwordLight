using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonInGame : MonoBehaviour
{
    public OnGamePauseManager onGamePauseManager;
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button?.onClick.AddListener(OnButtonClick);
    }
    void OnButtonClick()
    {
        onGamePauseManager?.Show();
    }
}
