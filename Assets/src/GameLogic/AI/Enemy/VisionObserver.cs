using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionObserver : MonoBehaviour
{
    public delegate void OnEnter();
    public event OnEnter OnEnter_;
    public delegate void OnExit();
    public event OnExit OnExit_;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnter_?.Invoke();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        OnExit_?.Invoke();
    }
}
