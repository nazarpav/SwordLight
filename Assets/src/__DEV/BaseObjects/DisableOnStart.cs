using UnityEngine;

public class DisableOnStart : MonoBehaviour
{
    public bool IsRealyDisableOnGameStart;
    void Start()
    {
        gameObject.SetActive(IsRealyDisableOnGameStart);
    }
}
