using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public GameObject StartPosition;
    public Collider2D LayerTop;
    public Collider2D LayerMiddle;
    public Collider2D LayerBottom;
    public float PosYOnTop;
    public float PosYOnMiddle;
    public float PosYOnBottom;
    public Rigidbody2D rigidbody;
    public Vector2 StartVelocity;
    public Vector2 ForceToUp;
    public Vector2 ForceToJump;
    public Vector2 ForceToFallDown;
    public string OnJumpTriggerName;
    public string OnAttackTriggerName;
    public string OnRunTriggerName;
    public string OnMakeDamageTriggerName;
    public string OnLayerMovementTriggerName;

    public RectTransform progressBar;
    public WLayers CurrentLayer { set; get; }
    public WLayers StartLayer;
    private bool _isOnGround;
    private void SetLayerOnStart()
    {
        if (StartLayer < TMP_CONSTANTS.minLayer || StartLayer > TMP_CONSTANTS.maxLayer)
        {
            CurrentLayer = WLayers.Medium;
        }
        else
        {
            CurrentLayer = StartLayer;
        }
        switch (CurrentLayer)
        {
            case WLayers.Bottom:
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, PosYOnBottom, gameObject.transform.position.z);
                break;
            case WLayers.Medium:
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, PosYOnMiddle, gameObject.transform.position.z);
                break;
            case WLayers.Top:
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, PosYOnTop, gameObject.transform.position.z);
                break;
        }
        UpdateLayersOn_Off();
    }
    private void UpdateLayersOn_Off()
    {
        animator.SetTrigger(OnLayerMovementTriggerName);
        LayerTop.enabled = false;
        LayerMiddle.enabled = false;
        LayerBottom.enabled = false;
        switch (CurrentLayer)
        {
            case WLayers.Bottom:
                LayerBottom.enabled = true;
                break;
            case WLayers.Medium:
                LayerMiddle.enabled = true;
                break;
            case WLayers.Top:
                LayerTop.enabled = true;
                break;
        }
    }
    private void UpdateRuntimeLayerUp()
    {
        if (CurrentLayer < TMP_CONSTANTS.maxLayer)
        {
            ++CurrentLayer;
            UpdateLayersOn_Off();
        }
    }
    private void UpdateRuntimeLayerDown()
    {
        --CurrentLayer;
        UpdateLayersOn_Off();
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
        Vector2 vector2 = progressBar.sizeDelta;
        vector2.x -= 10;
        progressBar.sizeDelta = vector2;
        animator.SetTrigger(OnMakeDamageTriggerName);
    }
    private void FixedUpdate()
    {
        StartVelocity.y = rigidbody.velocity.y;
        rigidbody.velocity = StartVelocity;

        if (progressBar.rect.width <= 0)
        {
            SceneManager.LoadScene("scev");
        }
    }
    private void OnLevelWin()
    {
        OnGameRestart_TMP();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Finish")
        {
        }
        else if (tag == "Damage")
        {
            OnDamageApplyed();
        }
        else if (tag == "WeaponFromEnemy")
        {
            OnDamageApplyed();
        }
        else if (tag == "Barrier")
        {
            OnDamageApplyed();
            OnGameOver();
        }
        else if (tag == "Ground")
        {
        }
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
            OnDamageApplyed();
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
        GameInput.instance.OnSwipeRight += OnForceFallDown;
        GameInput.instance.OnSwipeUp += OnMoveUp;
        GameInput.instance.OnTap += OnAttack;
    }
    void OnForceFallDown()
    {
        if (!_isOnGround)
        {
            // rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0.0f);
            rigidbody.AddForce(ForceToFallDown);
        }
    }
    void OnMoveUp()
    {
        if (CurrentLayer < TMP_CONSTANTS.maxLayer)
        {
            UpdateRuntimeLayerUp();
            if (_isOnGround)
            {
                switch (CurrentLayer)
                {
                    case WLayers.Bottom:
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x, PosYOnBottom, gameObject.transform.position.z);
                        break;
                    case WLayers.Medium:
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x, PosYOnMiddle, gameObject.transform.position.z);
                        break;
                    case WLayers.Top:
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x, PosYOnTop, gameObject.transform.position.z);
                        break;
                }
                //rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0.0f);
                //rigidbody.AddForce(ForceToUp);
                //Invoke("UpdateRuntimeLayerUp", 0.5f);
            }
        }
    }
    void OnMoveDown()
    {
        if (CurrentLayer > TMP_CONSTANTS.minLayer)
        {
            UpdateRuntimeLayerDown();
            if (_isOnGround)
            {
                switch (CurrentLayer)
                {
                    case WLayers.Bottom:
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x, PosYOnBottom, gameObject.transform.position.z);
                        break;
                    case WLayers.Medium:
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x, PosYOnMiddle, gameObject.transform.position.z);
                        break;
                    case WLayers.Top:
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x, PosYOnTop, gameObject.transform.position.z);
                        break;
                }
                //rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0.0f);
                ////rigidbody.AddForce(-ForceToUp/2);
                //UpdateRuntimeLayerDown();
            }
        }
    }
    void OnJump()
    {
        if (_isOnGround)
        {
            _isOnGround = false;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0.0f);
            animator.SetTrigger(OnJumpTriggerName);
            rigidbody.AddForce(ForceToJump);
        }
    }
    void OnAttack()
    {
        animator.SetTrigger(OnAttackTriggerName);
    }
}
