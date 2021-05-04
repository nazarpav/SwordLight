using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseCreature, IBaseCreatureMovable
{
    public EnemySpecifications specifications;
    private Vector3 _rScale = new Vector3(1, 1, 1);
    private Vector3 _lScale = new Vector3(-1, 1, 1);
    public float AttackDelayTime;
    public float MovedSpeed;
    public float Damage;
    public Vector2 _baseMovableFrame;
    public PlayerController Player;
    public EnemyTypes enemyType;
    public VisionObserver wvo;//wide vision observer
    public VisionObserver avo;//attack vision observer
    EnemyAI enemyAI;
    float _moveDT;
    public RectTransform health;
    private float _progressPointPerHpPoint;
    void Start()
    {
        LoadFromScrblObj();
        _progressPointPerHpPoint = health.sizeDelta.x / Health;
        enemyAI = new EnemyAI(_baseMovableFrame, wvo, avo, this, AttackDelayTime);
        _moveDT = 0;
    }
    void LoadFromScrblObj()
    {
        if (specifications == null || specifications.specifications == null)
        {
            return;
        }
        for (int i = 0; i < specifications.specifications.Length; i++)
        {
            if (specifications.specifications[i].type == enemyType)
            {
                var data = specifications.specifications[i].specification;
                Health = data.Health;
                AttackDelayTime = data.AttackFrequency;
                Damage = data.Damage;
                MovedSpeed = data.MoveSpeed;
                animator.runtimeAnimatorController = data.animator;
                break;
            }
        }
    }

    public void ChangeRuntimeLayer()
    {
        //if (Player.CurrentLayer == CurrentLayer) return;
        //CurrentLayer = Player.CurrentLayer;
        //switch (Player.CurrentLayer)
        //{
        //    case WLayers.Bottom:
        //        gameObject.transform.position = new Vector3(gameObject.transform.position.x, -2.85f, gameObject.transform.position.z);
        //        break;
        //    case WLayers.Medium:
        //        gameObject.transform.position = new Vector3(gameObject.transform.position.x, -1.85f, gameObject.transform.position.z);
        //        break;
        //    case WLayers.Top:
        //        gameObject.transform.position = new Vector3(gameObject.transform.position.x, -0.85f, gameObject.transform.position.z);
        //        break;
        //}
    }

    void Update()
    {
        if (movableState != BaseCreatureMovableStates.Death)
        {
            enemyAI.Update();
        }
    }

    public void Attack()
    {
        movableState = BaseCreatureMovableStates.Attack;
        animator.SetTrigger(OnAttackTriggerName);
        _moveDT = 0.0f;
    }

    public void Jump()
    {
        if (IsOnGround)
        {
            movableState = BaseCreatureMovableStates.Jump;
            IsOnGround = false;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0.0f);
            animator.SetTrigger(OnJumpTriggerName);
            rigidbody.AddForce(ForceToJump);
        }
    }
    public void Move()
    {
        _moveDT += Time.deltaTime;
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, Player.transform.position, _moveDT * MovedSpeed);
        if (movableState != BaseCreatureMovableStates.Move)
        {
            animator.SetTrigger(OnMoveTriggerName);
            movableState = BaseCreatureMovableStates.Move;
        }
        if (Player.transform.position.x - gameObject.transform.position.x > 0)
        {
            MoveRight();
        }
        else
        {
            MoveLeft();
        }
    }

    public void MoveLeft()
    {
        gameObject.transform.localScale = _lScale;
    }

    public void MoveRight()
    {
        gameObject.transform.localScale = _rScale;
    }

    public void MoveDown()
    {
        animator.SetTrigger(OnLayerMovementTriggerName);
    }

    public void MoveUp()
    {
        animator.SetTrigger(OnLayerMovementTriggerName);
    }

    protected override void OnCollisionBarrier()
    {
        OnMakedDamage();
        OnDeath();
    }

    protected override void OnFinish()
    {

    }

    protected override void OnMakedDamage()
    {
        Vector2 vector2 = health.sizeDelta;
        vector2.x -= _progressPointPerHpPoint;
        health.sizeDelta = vector2;
        movableState = BaseCreatureMovableStates.Damage;
        animator.SetTrigger(OnTakeDamageTriggerName);
        if (health.sizeDelta.x <= 0)
        {
            OnDeath();
        }
    }

    protected override void OnDeath()
    {
        movableState = BaseCreatureMovableStates.Death;
        animator.SetTrigger(OnDeathTriggerName);
        Destroy(gameObject, 0.5f);
    }
    public void Idle()
    {
        if (movableState != BaseCreatureMovableStates.Idle)
        {
            movableState = BaseCreatureMovableStates.Idle;
            animator.SetTrigger(OnIdleTriggerName);
        }
    }
}

class EnemyAI
{
    bool _isOnWideVision = false;
    bool _isOnAttackVision = false;
    Vector2 _baseMovableFrame;
    BaseEnemy _controlledEnemy;
    float _timeCounter;
    float _attackDelayTime;
    public EnemyAI(Vector2 baseMovableFrame, VisionObserver wvo, VisionObserver avo, BaseEnemy controllEnemy, float AttackDelayTime)
    {
        _attackDelayTime = AttackDelayTime;
        _controlledEnemy = controllEnemy;
        _baseMovableFrame = baseMovableFrame;
        wvo.OnEnter_ += OnPlayerEnteredWideVision;
        wvo.OnExit_ += OnPlayerExitWideVision;
        avo.OnEnter_ += OnPlayerEnteredAttackVision;
        avo.OnExit_ += OnPlayerExitAttackVision;
        _timeCounter = 0.0f;
    }
    public void Update()
    {
        _timeCounter += Time.deltaTime;
        if (_isOnAttackVision)
        {
            _controlledEnemy.ChangeRuntimeLayer();
            if (_timeCounter >= _attackDelayTime)
                _controlledEnemy.Attack();
        }
        else if (_isOnWideVision)
        {
            if (_isOnAttackVision == false)
            {
                _controlledEnemy.Move();
            }
        }
    }
    public void OnPlayerEnteredWideVision()
    {
        _isOnWideVision = true;
    }
    public void OnPlayerExitWideVision()
    {
        _isOnWideVision = false;
    }
    public void OnPlayerEnteredAttackVision()
    {
        _isOnAttackVision = true;
    }
    public void OnPlayerExitAttackVision()
    {
        _isOnAttackVision = false;
    }
}