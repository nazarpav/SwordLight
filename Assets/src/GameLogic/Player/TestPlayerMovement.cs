using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMovement : MonoBehaviour
{
    void Start()
    {
        GameInput.instance.OnSwipeDown += moveDown;
        GameInput.instance.OnSwipeUp += moveUp;
        GameInput.instance.OnSwipeRight += moveRight;
        GameInput.instance.OnSwipeLeft += moveLeft;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void moveUp()
    {
        Debug.Log("Up");
        transform.position = transform.position + new Vector3(0.0f, 10.0f, 0.0f);
        transform.localScale = transform.localScale + new Vector3(-1.0f, -1.0f, 0.0f);
    }
    void moveDown()
    {
        transform.position = transform.position + new Vector3(0.0f, -10.0f, 0.0f);
        transform.localScale = transform.localScale + new Vector3(1.0f, 1.0f, 0.0f);
        Debug.Log("Down");
    }
    void moveLeft()
    {
        transform.position = transform.position + new Vector3( - 10.0f, 0.0f, 0.0f);
        Debug.Log("Left");
    }
    void moveRight()
    {
        transform.position = transform.position + new Vector3(10.0f, 0.0f, 0.0f);
        Debug.Log("Right");
    }
}
