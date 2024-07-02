using Godot;

namespace BarbarianBlaster.enemy;

public partial class EnemyPath : Path3D
{
    private PackedScene _enemyScene = ResourceLoader.Load<PackedScene>("res://enemy/enemy.tscn");

    private Timer? _timer;

    private Timer Timer
    {
        get
        {
            if (_timer is not null) return _timer;
            _timer = GetNode<Timer>("Timer");
            return _timer;
        }
    }

    private void SpawnEnemy()
    {
        var newEnemy = _enemyScene.Instantiate();
        AddChild(newEnemy);
    }

    public override void _Ready()
    {
        base._Ready();

        Timer.Timeout += SpawnEnemy;
        Timer.WaitTime = 2;
        Timer.Start();
    }
}