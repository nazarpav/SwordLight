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
        float horAxis = Input.GetAxis("Horizontal");
        float verAxis = Input.GetAxis("Vertical");
        if (OnSwipeLeft != null && horAxis > 0)
        {
            OnSwipeRight.Invoke();
        }
        else if (OnSwipeRight != null && horAxis != 0)
        {
            OnSwipeLeft.Invoke();
        }
        if (OnSwipeUp != null && verAxis > 0)
        {
            OnSwipeUp.Invoke();
        }
        else if (OnSwipeDown != null && verAxis != 0)
        {
            OnSwipeDown.Invoke();
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
            //Detects Swipe while finger is still moving
            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeOnlyAfterRelease)
                {
                    fingerDown = touch.position;
                    checkSwipe();
                }
            }
            //Detects swipe after finger is released
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                checkSwipe();
            }
        }
    }

    void checkSwipe()
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