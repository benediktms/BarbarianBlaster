using Godot;

namespace BarbarianBlaster.DifficultyManager;

public partial class DifficultyManager : Node
{
    [Export] public required Curve SpawnRateCurve { get; set; }
    [Export] private Curve _healthCurve = null!;

    private Timer _timer = null!;

    private float _gameLength = 40;

    public override void _Ready()
    {
        base._Ready();

        _timer = GetNode<Timer>("Timer");
        _timer.OneShot = true;
        _timer.Start(_gameLength);
    }

    private float GameProgressRatio()
    {
        var ratio = (float)_timer.TimeLeft / _gameLength;

        return 1 - ratio;
    }

    public float UpdateSpawnRate() => SpawnRateCurve.Sample(GameProgressRatio());

    public int UpdateEnemyHealth() => (int)_healthCurve.Sample(GameProgressRatio());
}