using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _baseRunSpeed;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private ParticleSystem _particleSystem;

    [SerializeField] private AudioClip _bonusClip;
    [SerializeField] private AudioClip _cheering;
    [SerializeField] private AudioClip _enemyClip;

    private UIValues _uiValues;

    private Vector3 _velocity = Vector3.zero;
    float _moveInput;
    private float _runSpeed;

    private bool _moving = false;
    private bool finishedGame = false;
    int _coins;
    float _maxPosition;

    private bool _activatedSpeed = false;

    [SerializeField] BonusSlider _speedBonus;
    [SerializeField] BonusButton _tackleBonus;
    [SerializeField] BonusButton _jumpBonus;

    public void StartGame()
    {
        Move(true);
    }

    private bool _canPerformActions = true;

    public void PerformAction(bool active)
    {
        _canPerformActions = active;
    }

    private void Awake()
    {
        GameData.CurrentLevel = 0;
        Application.targetFrameRate = 60;
        StartGame();
        int baseSize = 100;
        baseSize += GameData.InsreasePerLevel * GameData.CurrentLevel;
        _maxPosition = baseSize;
        GameData.Target = transform;

        _uiValues = FindObjectOfType<UIValues>();
        _runSpeed = baseSize / 20f;
        SetBonusValues();
    }

    private void SetBonusValues()
    {
        /*_speedBonus.CurrentValue = _speedBonus.CurrentValueSaved;
        _speedBonus.MaxValue = _speedBonus.MaxValueSaved;

        _tackleBonus.CurrentValue = _tackleBonus.CurrentValueSaved;
        _tackleBonus.MaxValue = _tackleBonus.MaxValueSaved;

        _jumpBonus.CurrentValue = _jumpBonus.CurrentValueSaved;
        _jumpBonus.MaxValue = _jumpBonus.MaxValueSaved;*/

        _uiValues.SetJumpCount(_jumpBonus.CurrentValue);
        _uiValues.SetTurnCount(_tackleBonus.CurrentValue);
        _uiValues.SetSpeedSlider(_speedBonus.CurrentValue / _speedBonus.MaxValue);
    }

    private void Update()
    {
        if (finishedGame) return;

        if (transform.position.y>= _maxPosition)
        {
            WonLevel();
        }

        if (Input.GetKey(_speedBonus.KeyCode))
        {
            ActivateSpeedBonus();
        }

        if (Input.GetKeyDown(_tackleBonus.KeyCode))
        {
            TackleBonus();
        }
        if (Input.GetKeyDown(_jumpBonus.KeyCode))
        {
            JumpBonus();
        }

        _uiValues.SetCounterText($"Yards: {(int)transform.position.y}");
        _moveInput = SimpleInput.GetAxisRaw("Horizontal");
        if (_moveInput<0)
        {
            _moveInput = -1f;
        }
        else if (_moveInput>0)
        {
            _moveInput = 1f;
        }
    }

    public void JumpBonus()
    {
        if (_canPerformActions && _jumpBonus.CurrentValue > 0)
        {
            _jumpBonus.CurrentValue--;
            _uiValues.SetJumpCount(_jumpBonus.CurrentValue);
            _animator.Play("jump");
        }
    }

    public void TackleBonus()
    {
        if (_canPerformActions && _tackleBonus.CurrentValue > 0)
        {
            _tackleBonus.CurrentValue--;
            _uiValues.SetTurnCount(_tackleBonus.CurrentValue);
            _animator.Play("turn");
        }
    }

    public void ActivateSpeedBonus()
    {
        if ((_canPerformActions && _speedBonus.CurrentValue > 0))
        {
            _speedBonus.Active = true;
            _speedBonus.CurrentValue -= Time.deltaTime;

            _uiValues.SetSpeedSlider(_speedBonus.CurrentValue / _speedBonus.MaxValue);
            if (_speedBonus.CurrentValue <= 0)
            {
                _speedBonus.Active = false;
            }
        }
    }

    private void WonLevel()
    {
        //finishedGame = true;
        //Enemy.OnAttackedPlayerTrigger(null);
        _coins += 6;
        _particleSystem.Play();
        _uiValues.SetCoinCount(_coins);
        GameSounds.OnChoseSoundTrigger(_cheering, 1f);
        //Invoke(nameof(FinishedLevel), 2f);
        GameData.NextLevel();
        int baseSize = 100 + GameData.InsreasePerLevel * GameData.CurrentLevel;
        _maxPosition += baseSize;
        _uiValues.NextTouchDown((int)_maxPosition);
        _runSpeed = baseSize / 20f;
        LevelGenerator.OnTouchDownTrigger();

    }

    private void Lost()
    {
        GameOver.OnGameFinishedTrigger(false);
    }

    void FixedUpdate()
    {
        if (finishedGame) return;

        if (!_moving) return;
        _velocity.x = _moveSpeed * _moveInput * Time.deltaTime;

        float speed = _runSpeed;
        if (_speedBonus.Active)
        {
            speed *= 1.5f;
        }
        _velocity.y = speed * Time.deltaTime;
        Vector3 nextPosition = _velocity + _rigidbody2D.transform.position;
        nextPosition.x = Mathf.Clamp(nextPosition.x, -2, 2);
        _rigidbody2D.transform.position = nextPosition;


        _speedBonus.Active = false;
    }

    public void Jump(bool active)
    {
        _animator.SetBool("Jump", active);
    }
    public void Tackle(bool active)
    {
        _animator.SetBool("Tackle", active);
    }
    public void Move(bool active)
    {
        _animator.SetBool("Run", active);
        _moving = active;
    }

    public void Lose()
    {
        GameData.SetHighscore(_coins);
        GameData.PublishHighscore();
        _animator.Play("down");
        GameData.RestartLevel();
        Invoke(nameof(Lost), 2f);
        finishedGame = true;
        _moving = false;
        _animator.SetBool("Lost", true);
    }

    public void FillSpeedBoost()
    {
        _speedBonus.CurrentValue = _speedBonus.MaxValue;
        _uiValues.SetSpeedSlider(_speedBonus.CurrentValue / _speedBonus.MaxValue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && _canPerformActions)
        {
            GameSounds.OnChoseSoundTrigger(_enemyClip, 1f);
            Enemy.OnAttackedPlayerTrigger(collision.GetComponent<Enemy>());
            Lose();
        }

        if (collision.CompareTag("Speed"))
        {
            GameSounds.OnChoseSoundTrigger(_bonusClip, 1f);
            FillSpeedBoost();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Turn"))
        {
            GameSounds.OnChoseSoundTrigger(_bonusClip, 1f);
            _tackleBonus.CurrentValue++;
            _uiValues.SetTurnCount(_tackleBonus.CurrentValue);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Jump"))
        {
            GameSounds.OnChoseSoundTrigger(_bonusClip, 1f);
            _jumpBonus.CurrentValue++;
            _uiValues.SetJumpCount(_jumpBonus.CurrentValue);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Bonus"))
        {
            GameSounds.OnChoseSoundTrigger(_bonusClip, 1f);
            Destroy(collision.gameObject);

            _coins += 2;
            _uiValues.SetCoinCount(_coins);

        }
    }


}
[System.Serializable]
public class BonusSlider
{
    public string Name;
    public KeyCode KeyCode;
    public bool Active = false;
    public float CurrentValue;
    public float MaxValue;
    public float MaxValueSaved => PlayerPrefs.GetFloat(Name+"_time", BaseValue);
    public float CurrentValueSaved => PlayerPrefs.GetFloat(Name+"_current", BaseValue);
    public float BaseValue = 2;


    public void SaveCurrentValues()
    {
        PlayerPrefs.SetFloat(Name + "_current", CurrentValue);
    }
    public void UpgradeMaxValue(float value)
    {
        PlayerPrefs.SetFloat(Name + "_time", value);
    }
}
[System.Serializable]
public class BonusButton
{
    public string Name;
    public KeyCode KeyCode;
    public int CurrentValue;
    public int MaxValue;
    public int CurrentValueSaved => PlayerPrefs.GetInt(Name+"_current", BaseValue);
    public int MaxValueSaved => PlayerPrefs.GetInt(Name+"_time", BaseValue);
    public int BaseValue = 2;

    public void SaveCurrentValues()
    {
        PlayerPrefs.SetInt(Name + "_current", CurrentValue);
    }
    public void UpgradeMaxValue(int value)
    {
        PlayerPrefs.SetInt(Name + "_time", value);
    }
}
