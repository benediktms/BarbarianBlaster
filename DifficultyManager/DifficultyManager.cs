using Godot;

namespace BarbarianBlaster.DifficultyManager;

public partial class DifficultyManager : Node
{
    [Export] private Curve _spawnRateCurve = null!;
    [Export] private Curve _healthCurve = null!;

    private float _gameLength = 40;
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

    public float SpawnTime { get; private set; }
    public int EnemyHealth { get; private set; }

    public override void _Ready()
    {
        base._Ready();

        Timer.OneShot = true;
        Timer.Start(_gameLength);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        (SpawnTime, EnemyHealth) = UpdateFromCurves();
    }

    private float GameProgressRatio()
    {
        var ratio = (float)Timer.TimeLeft / _gameLength;

        return 1 - ratio;
    }

    private (float SpawnRateSample, int EnemyHealthSample) UpdateFromCurves()
    {
        var spawnRate = _spawnRateCurve.Sample(GameProgressRatio());
        var enemyHealth = (int)(_healthCurve.Sample(GameProgressRatio()));

        return (spawnRate, enemyHealth);
    }
}