using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLevelsData : MonoBehaviour
{
    public static GlobalLevelsData instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
