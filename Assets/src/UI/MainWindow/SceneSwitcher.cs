using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    public void SwitchLevel(int levelNumber)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("__DEV");
    }
}
