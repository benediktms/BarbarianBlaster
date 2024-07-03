using Godot;

namespace BarbarianBlaster.enemy;

using DifficultyManager;

public partial class EnemyPath : Path3D
{
    [Export] private DifficultyManager _difficultyManager = null!;

    private PackedScene _enemyScene = ResourceLoader.Load<PackedScene>("res://enemy/enemy.tscn");

    private Timer? _timer;


    private void SpawnEnemy()
    {
        var enemy = _enemyScene.Instantiate<Enemy>();
        enemy.MaxHealth = _difficultyManager.EnemyHealth;
        AddChild(enemy);
    }

    public override void _Ready()
    {
        base._Ready();

        _timer = GetNode<Timer>("Timer");

        _timer.Timeout += SpawnEnemy;
        _timer.WaitTime = _difficultyManager.SpawnTime;
        _timer.Start();
    }
}