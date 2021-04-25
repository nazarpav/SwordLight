using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public GameObject StartPosition;
    public GameObject LayerTop;
    public GameObject LayerMiddle;
    public GameObject LayerBottom;
    public float PosYOnTop;
    public float PosYOnMiddle;
    public float PosYOnBottom;
    public Rigidbody2D rigidbody;
    public Vector2 StartVelocity;
    public Vector2 ForceToUp;
    public Vector2 ForceToJump;
    public string OnJumpTriggerName;
    public string OnAttackTriggerName;
    public string OnRunTriggerName;
    public string OnMakeDamageTriggerName;
    public string OnLayerMovementTriggerName;
    private int _currentLayer;
    public int StartLayer;
    private int minLayer = 0;
    private int maxLayer = 2;
    private bool _isOnGround;
    private void SetLayerOnStart()
    {
        if (StartLayer < minLayer || StartLayer > maxLayer)
        {
            _currentLayer = minLayer + maxLayer / 2;
        }
        else
        {
            _currentLayer = StartLayer;
        }
        switch (_currentLayer)
        {
            case 0:
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, PosYOnBottom, gameObject.transform.position.z);
                break;
            case 1:
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, PosYOnMiddle, gameObject.transform.position.z);
                break;
            case 2:
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, PosYOnTop, gameObject.transform.position.z);
                break;
        }
    }
    private void UpdateLayersOn_Off()
    {
        animator.SetTrigger(OnLayerMovementTriggerName);
        switch (_currentLayer)
        {
            case 0:
                LayerTop.SetActive(false);
                LayerMiddle.SetActive(false);
                LayerBottom.SetActive(true);
                break;
            case 1:
                LayerTop.SetActive(false);
                LayerMiddle.SetActive(true);
                LayerBottom.SetActive(false);
                break;
            case 2:
                LayerTop.SetActive(true);
                LayerMiddle.SetActive(false);
                LayerBottom.SetActive(false);
                break;
        }
    }
    private void UpdateRuntimeLayer(bool isUp)
    {
        if (isUp && _currentLayer < maxLayer)
        {
            rigidbody.AddForce(ForceToUp);
            ++_currentLayer;
            UpdateLayersOn_Off();
        }
        else if (isUp == false && _currentLayer > minLayer)
        {
            --_currentLayer;
            UpdateLayersOn_Off();
        }
    }
    void Start()
    {
        InitInput();
        SetLayerOnStart();
        ForceToUp.x = 0;
        OnGameRestart_TMP();
    }
    private void OnGameRestart_TMP()
    {
        gameObject.transform.position = new Vector3(StartPosition.transform.position.x, StartPosition.transform.position.y, gameObject.transform.position.z);
    }
    private void OnGameOver()
    {
        animator.SetTrigger(OnMakeDamageTriggerName);
        OnGameRestart_TMP();
    }
    private void OnDamageApplyed()
    {
        animator.SetTrigger(OnMakeDamageTriggerName);
    }
    private void FixedUpdate()
    {
        StartVelocity.y = rigidbody.velocity.y;
        rigidbody.velocity = StartVelocity;
    }
    private void OnLevelWin()
    {
        OnGameRestart_TMP();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isOnGround = false;
        string tag = collision.gameObject.tag;
        if (tag == "Finish")
        {
            OnLevelWin();
            OnDamageApplyed();
        }
        else if (tag == "Damage")
        {
            OnDamageApplyed();
        }
        else if (tag == "Barrier")
        {
            OnGameOver();
        }
        else if (tag == "Ground")
        {
            _isOnGround = true;
        }
    }
    void InitInput()
    {
        GameInput.instance.OnSwipeDown += OnMoveDown;
        GameInput.instance.OnSwipeLeft += OnJump;
        GameInput.instance.OnSwipeRight += OnAttack;
        GameInput.instance.OnSwipeUp += OnMoveUp;
    }
    void OnMoveUp()
    {
        UpdateRuntimeLayer(true);
    }
    void OnMoveDown()
    {
        UpdateRuntimeLayer(false);
    }
    void OnJump()
    {
        if (_isOnGround)
        {
            _isOnGround = false;
            animator.SetTrigger(OnJumpTriggerName);
            rigidbody.AddForce(ForceToJump);
        }
    }
    void OnAttack()
    {
        animator.SetTrigger(OnAttackTriggerName);
    }
}
