using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool detectSwipeOnlyAfterRelease = false;
    public delegate void SwipeUp();
    public event SwipeUp OnSwipeUp;
    public delegate void SwipeDown();
    public event SwipeDown OnSwipeDown;
    public delegate void SwipeLeft();
    public event SwipeLeft OnSwipeLeft;
    public delegate void SwipeRight();
    public event SwipeRight OnSwipeRight;
    public delegate void Tap();
    public event Tap OnTap;
    public float SWIPE_THRESHOLD = 20f;

    private delegate void SwipeCheckCallback();
    SwipeCheckCallback __swipeDetectCallback = null;
    public static GameInput instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    private void Start()
    {
        RuntimePlatform platform = Application.platform;
        if (platform == RuntimePlatform.WebGLPlayer || platform == RuntimePlatform.WindowsPlayer ||
            platform == RuntimePlatform.WindowsEditor || platform == RuntimePlatform.OSXEditor ||
            platform == RuntimePlatform.OSXPlayer || platform == RuntimePlatform.LinuxEditor ||
             platform == RuntimePlatform.LinuxPlayer || platform == RuntimePlatform.PS5 || platform == RuntimePlatform.PS4)
        {
            __swipeDetectCallback = DesktopSwipeCheck;
        }
        else
        {
            __swipeDetectCallback = PhoneSwipeCheck;
        }
    }
    void Update()
    {
        __swipeDetectCallback.Invoke();
    }
    void DesktopSwipeCheck()
    {
        if (OnSwipeLeft != null && Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnSwipeRight.Invoke();
        }
        else if (OnSwipeRight != null && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnSwipeLeft.Invoke();
        }
        if (OnSwipeUp != null && Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnSwipeUp.Invoke();
        }
        else if (OnSwipeDown != null && Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnSwipeDown.Invoke();
        }
        if(OnTap != null && Input.GetKeyDown(KeyCode.Space))
        {
            OnTap.Invoke();
        }
    }
    void PhoneSwipeCheck()
    {
        for (int i = 0; i < Input.touches.Length; i++)
        {
            var touch = Input.touches[i];
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }
            ////Detects Swipe while finger is still moving
            //if (touch.phase == TouchPhase.Moved)
            //{
            //    if (!detectSwipeOnlyAfterRelease)
            //    {
            //        fingerDown = touch.position;
            //        CheckSwipeOrClick();
            //    }
            //}
            //Detects swipe after finger is released
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                CheckSwipeOrClick();
            }
        }
    }

    void CheckSwipeOrClick()
    {
        //Check if Vertical swipe
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
        {
            //Debug.Log("Vertical");
            if (OnSwipeUp != null && fingerDown.y - fingerUp.y > 0)//up swipe
            {
                OnSwipeUp.Invoke();
            }
            else if (OnSwipeDown != null && fingerDown.y - fingerUp.y < 0)//Down swipe
            {
                OnSwipeDown.Invoke();
            }
            fingerUp = fingerDown;
        }
        //Check if Horizontal swipe
        else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            //Debug.Log("Horizontal");
            if (OnSwipeRight != null && fingerDown.x - fingerUp.x > 0)//Right swipe
            {
                OnSwipeRight.Invoke();
            }
            else if (OnSwipeLeft != null && fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                OnSwipeLeft.Invoke();
            }
            fingerUp = fingerDown;
        }
        else
        {
            OnTap.Invoke();
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }
}