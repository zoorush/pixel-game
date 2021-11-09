using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action<Enemy> OnAttackedPlayer;


    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _baseRunSpeed;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _detectionRadius = 5;
    [SerializeField] private LayerMask _whatIsPlayer;
    private Vector3 _velocity = Vector3.zero;
    float _moveInput;
    private float _runSpeed;

    private bool _moving = false;

    private Transform _target;


    private void Awake()
    {
        _runSpeed = _baseRunSpeed;
        OnAttackedPlayer += AttackedPlayer;
    }

    private void Update()
    {
        /*if (!_target)
        {
            Collider2D player = Physics2D.OverlapCircle(transform.position, _detectionRadius, _whatIsPlayer);
            if (player)
            {
                _target = player.transform;
                _moving = true; Move(true);
            }
        }*/

        if (!_target)
        {
            if (GameData.Target)
            {
                if (Vector2.Distance(transform.position,GameData.Target.position)<=_detectionRadius)
                {
                    _target = GameData.Target;
                    _moving = true; Move(true);
                }
            }
        }
        else
        {
            if (transform.position.y < _target.position.y - 5)
            {
                Destroy(gameObject);
                return;
            }
        }


    }

    void FixedUpdate()
    {
        if (!_moving) return;

        float speed = _runSpeed;

        _velocity.y = speed * Time.deltaTime * -1;
        Vector3 nextPosition = _velocity + _rigidbody2D.transform.position;
        nextPosition.x = Mathf.MoveTowards(nextPosition.x, _target.position.x, Time.deltaTime * _moveSpeed);
        nextPosition.x = Mathf.Clamp(nextPosition.x, -2, 2);
        _rigidbody2D.transform.position = nextPosition;
    }

    public static void OnAttackedPlayerTrigger(Enemy enemy)
    {
        OnAttackedPlayer?.Invoke(enemy);
    }

    public void Down()
    {
        _moving = false;
        _animator.SetBool("Down", true);
    }

    public void Lost()
    {
        Move(false);
    }
    public void Move(bool active)
    {
        _animator.SetBool("Run", active);
        _moving = active;
    }

    private void OnDrawGizmos()
    {
        if (_target)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
    private void OnDestroy()
    {
        OnAttackedPlayer -= AttackedPlayer;
    }

    private void AttackedPlayer(Enemy enemy)
    {
        if (enemy == this)
        {
            _animator.SetTrigger("Celebrate");
        }
        else
        {
            Move(false);
        }
    }
}
