using Godot;

namespace BarbarianBlaster.enemy;
using UI;

using DifficultyManager;

public partial class EnemyPath : Path3D
{
    [Export] private DifficultyManager _difficultyManager = null!;
    [Export] private VictoryLayer _victoryLayer = null!;

    private PackedScene _enemyScene = ResourceLoader.Load<PackedScene>("res://enemy/enemy.tscn");

    private Timer _timer = null!;

    private void SpawnEnemy()
    {
        var enemy = _enemyScene.Instantiate<Enemy>();
        enemy.MaxHealth = _difficultyManager.UpdateEnemyHealth();
        AddChild(enemy);
        enemy.TreeExited += EnemyDefeated;

        _timer.WaitTime = _difficultyManager.UpdateSpawnRate();
    }

    public override void _Ready()
    {
        base._Ready();

        _timer = GetNode<Timer>("Timer");
        _timer.WaitTime = _difficultyManager.SpawnRateCurve.MaxValue;
        _timer.Timeout += SpawnEnemy;
        _timer.Start();

        _difficultyManager.StopSpawningEnemies += OnStopSpawningEnemies;
    }

    private void OnStopSpawningEnemies()
    {
        _timer.Stop();
    }

    private void EnemyDefeated()
    {
        if (_timer.IsStopped() is false) return;

        foreach (var child in GetChildren())
        {
            if (child is Enemy) return;
        }

        _victoryLayer.Victory();
    }
}