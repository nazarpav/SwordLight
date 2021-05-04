using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WLayers
{
    Bottom = 0,
    Medium,
    Top
}
public class TMP_CONSTANTS
{
    public const WLayers minLayer = WLayers.Bottom;
    public const WLayers maxLayer = WLayers.Top;
}
public abstract class BaseCreature : MonoBehaviour
{
    protected BaseCreatureMovableStates movableState;
    public Animator animator;
    public GameObject StartPosition;
    public Rigidbody2D rigidbody;
    public float Health;
    public Vector2 ForceToUp;
    public Vector2 ForceToJump;
    public Vector2 ForceToFallDown;
    public string OnIdleTriggerName;
    public string OnJumpTriggerName;
    public string OnAttackTriggerName;
    public string OnMoveTriggerName;
    public string OnTakeDamageTriggerName;
    public string OnLayerMovementTriggerName;
    public string OnDeathTriggerName;
    public WLayers CurrentLayer { set; get; }
    public WLayers StartLayer;
    public bool IsOnGround { set; get; }
    protected abstract void OnFinish();
    protected abstract void OnDeath();
    protected abstract void OnMakedDamage();
    protected abstract void OnCollisionBarrier();
    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Finish":
                OnFinish();
                break;
            case "WeaponFromPlayer":
            case "Damage":
                OnMakedDamage();
                break;
            case "Barrier":
                OnCollisionBarrier();
                break;
            case "Ground":
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsOnGround = false;
        switch (collision.gameObject.tag)
        {
            case "Finish":
                OnFinish();
                break;
            case "Damage":
                OnMakedDamage();
                break;
            case "Barrier":
                OnCollisionBarrier();
                break;
            case "Ground":
                IsOnGround = true;
                break;
        }
    }
}
public enum BaseCreatureMovableStates
{
    Idle,
    Move,
    Jump,
    Attack,
    Death,
    Damage
}
interface IBaseCreatureMovable
{
    void Idle();
    void MoveUp();
    void MoveDown();
    void MoveLeft();
    void MoveRight();
    void Jump();
    void Attack();
}
