using System;
using UnityEngine;
using static AsteroidsPool;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private GameObject playerShip;
    [SerializeField] private int targetPointForNewHp;

    public event Action<int> OnScoreChange;
    
    public int PlayerHealth { get; private set; }
    
    private HealthComponent _playerHealthComponent;
    private int _score;
    private int _initTargetPoint;

    public void SubscribeOnHealthChange(Action<int> func) => _playerHealthComponent.OnChangeHealth += func;
    public void UnSubscribeOnHealthChange(Action<int> func) => _playerHealthComponent.OnChangeHealth -= func;
    
    private void Start()
    {
        _playerHealthComponent = playerShip.GetComponent<HealthComponent>();
        PlayerHealth = _playerHealthComponent.Health;
        _initTargetPoint = targetPointForNewHp;
        _playerHealthComponent.OnChangeHealth += ChangeHealth;
        _playerHealthComponent.OnObjectDestroy += EndGame;
        OnAsteroidDead += GetPointForAsteroid;
    }

    private void ChangeHealth(int value) => PlayerHealth = value;

    private void OnDestroy()
    {
        OnAsteroidDead -= GetPointForAsteroid;
        _playerHealthComponent.OnChangeHealth -= ChangeHealth;
        _playerHealthComponent.OnObjectDestroy -= EndGame;
    }

    private void GetPointForAsteroid(int value)
    {
        _score += value;
        OnScoreChange?.Invoke(_score);
        if(_score < targetPointForNewHp) return;
        _playerHealthComponent.IncreaseHealthPoint();
        targetPointForNewHp += _initTargetPoint;
    }

    private void EndGame() => Time.timeScale = 0;
}