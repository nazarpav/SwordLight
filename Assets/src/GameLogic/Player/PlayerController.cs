using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public GameObject StartPosition;
    public Rigidbody2D rigidbody;
    public float RunVelocity;
    public float MaxVelocity;
    public Vector2 ForceToUp;
    public Vector2 ForceToJump;
    public Vector2 ForceToFallDown;
    public string OnJumpTriggerName;
    public string OnAttackTriggerName;
    public string OnRunTriggerName;
    public string OnMakeDamageTriggerName;
    public string OnLayerMovementTriggerName;
    private bool faceRight = true;

    public RectTransform progressBar;
    public WLayers CurrentLayer { set; get; }
    public WLayers StartLayer;
    private bool _isOnGround;
    void Start()
    {
        InitInput();
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
        //StartVelocity.y = rigidbody.velocity.y;
        //rigidbody.velocity = StartVelocity;

        if (progressBar.rect.width <= 0)
        {
            SceneManager.LoadScene("scev");
        }

        float moveX = Input.GetAxis("Horizontal");
        rigidbody.velocity = new Vector2(Mathf.Clamp(rigidbody.velocity.x + moveX * RunVelocity * Time.deltaTime, -MaxVelocity, MaxVelocity), rigidbody.velocity.y);
        if (moveX > 0 && !faceRight)
            flip();
        else if (moveX < 0 && faceRight)
            flip();
    }

    private void flip()
    {
        faceRight = !faceRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
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
        GameInput.instance.OnSwipeUp += OnJump;
        GameInput.instance.OnSwipeDown += OnForceFallDown;
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

    }
    void OnMoveDown()
    {

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
