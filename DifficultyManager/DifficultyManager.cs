using Godot;

namespace BarbarianBlaster.DifficultyManager;

public partial class DifficultyManager : Node
{
    [Export] private Curve _curve = null!;

    private float _gameLength = 30;
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

    public override void _Ready()
    {
        base._Ready();

        Timer.OneShot = true;
        Timer.Start(_gameLength);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        SpawnTime = UpdateSpawnTime();
    }

    private float GameProgressRatio()
    {
        var ratio = (float)Timer.TimeLeft / _gameLength;

        return 1 - ratio;
    }

    private float UpdateSpawnTime()
    {
        return _curve.Sample(GameProgressRatio());
    }
}