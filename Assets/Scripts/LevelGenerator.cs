using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static event System.Action OnTouchDown;


    [SerializeField] private GameObject[] _levelPrefab;
    [SerializeField] private GameObject _midPrefab;
    [SerializeField] private GameObject _finalPrefab;
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private float _xClamp;
    [SerializeField] private GameObject _bonusBall;
    [SerializeField] private GameObject _speedBonus;
    [SerializeField] private GameObject _jumpBonus;
    [SerializeField] private GameObject _turnBonus;

    [SerializeField] private GameObject[] _players;

    private Vector3 _latestPosition = Vector3.zero;

    Player player;
    void Awake()
    {
        player = Instantiate(_players[GameData.SelectedCharacter], Vector3.zero, Quaternion.identity).GetComponent<Player>();
        OnTouchDown += GenerateNewPart;
        int baseSize = 100;
        baseSize += GameData.InsreasePerLevel * GameData.CurrentLevel;
        GenerateEnemies(baseSize);
        GenerateBonus(baseSize);
        GenerateLevel(baseSize);
    }

    public static void OnTouchDownTrigger()
    {
        OnTouchDown?.Invoke();
    }

    public void GenerateNewPart()
    {
        if (GameData.CurrentLevel>=10)
        {
            GameOver.OnGameFinishedTrigger(true);
            return;
        }
        int baseSize = 100;
        baseSize += GameData.InsreasePerLevel * GameData.CurrentLevel;
        GenerateEnemies(baseSize);
        GenerateBonus(baseSize);
        GenerateLevel(baseSize);
    }

    public void InitPlayer()
    {
        player.StartGame();
    }

    private void GenerateBonus(int baseSize)
    {

        Vector3 minSpawn = _latestPosition +  Vector3.up * 10;
        Vector3 maxSpawn = _latestPosition + Vector3.up*(baseSize-10);
        int random = Random.Range(3, 6);
        for (int i = 0; i < random; i++)
        {
            Instantiate(_bonusBall, new Vector3(Random.Range(-2, 2f), Random.Range(minSpawn.y, maxSpawn.y), 0), Quaternion.identity);
        }

        if (Random.Range(0,1f)>.25f)
        {
            Instantiate(_jumpBonus, new Vector3(Random.Range(-2, 2f), Random.Range(minSpawn.y, maxSpawn.y), 0), Quaternion.identity);
        }


        for (int i = 0; i < 1; i++)
        {
            if (Random.Range(0, 1f) > .5)
            {
                Instantiate(_speedBonus, new Vector3(Random.Range(-2, 2f), Random.Range(minSpawn.y, maxSpawn.y), 0), Quaternion.identity);
            }
        }
        for (int i = 0; i < 1; i++)
        {
            if (Random.Range(0, 1f) > .5)
            {
                Instantiate(_turnBonus, new Vector3(Random.Range(-2, 2f), Random.Range(minSpawn.y, maxSpawn.y), 0), Quaternion.identity);
            }
        }
    }

    private void GenerateLevel(int baseSize)
    {
        Vector3 _startPosition = _latestPosition;
        int count = baseSize / 5;
        for (int i = 0; i < count; i++)
        {
            GameObject go;
            if (i == count / 2)
            {
                go = _midPrefab;
            }
            else
            {
                go = _levelPrefab[Random.Range(0, _levelPrefab.Length)];
            }
            Instantiate(go, _startPosition, Quaternion.identity);
            _startPosition.y += 5f;
        }

        _latestPosition = _startPosition;
        Instantiate(_finalPrefab, _startPosition, Quaternion.identity);
    }

    public void GenerateEnemies(int baseSize)
    {
        Vector3 _startPosition = _latestPosition + Vector3.up * 10; 
        int count = baseSize / 5;
        for (int i = 3; i < count; i++)
        {
            Instantiate(_enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)], new Vector3(Random.Range(-_xClamp, _xClamp), _startPosition.y + Random.Range(-1, 1f), 0), Quaternion.identity);
            _startPosition.y += 5f;
        }
    }

    private void OnDestroy()
    {
        OnTouchDown -= GenerateNewPart;
    }
}
