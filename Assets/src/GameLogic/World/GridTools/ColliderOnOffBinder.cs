using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderOnOffBinder : MonoBehaviour
{
    public Collider2D CurrentCollider;
    public Collider2D BindableCollider;
    private bool isIEnable;
    void Start()
    {
        isIEnable = BindableCollider.enabled;
        CurrentCollider.enabled = isIEnable;
    }
    void Update()
    {
        if (BindableCollider.enabled != isIEnable)
        {
            isIEnable = !isIEnable;
            CurrentCollider.enabled = isIEnable;
        }
    }
}
