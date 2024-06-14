using Godot;

namespace BarbarianBlaster.turret;

public partial class Turret : Node3D
{
    private Path3D? _path;

    public Path3D? Path
    {
        get => _path;
        set
        {
            if (value is null) return;

            _path = value;
        }
    }

    [Export] private PackedScene? _projectile;
    private Timer? _timer;
    private MeshInstance3D? _barrel;

    private Enemy? _target;

    private float _turretRange = 10;

    public override void _Ready()
    {
        base._Ready();

        _timer = GetNode<Timer>("Timer");
        _timer.Timeout += OnTimerTimeout;
        _timer.Start();

        _barrel = GetNode<MeshInstance3D>("TurretBase/TurretTop/Barrel");
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        _target = FindBestTarget();
        if (_target is not null)
        {
            LookAt(_target.GlobalPosition, Vector3.Up, true);
        }
    }

    private Enemy? FindBestTarget()
    {
        Enemy? bestTarget = null;
        float bestProgress = 0;

        var nodes = Path?.GetChildren();
        if (nodes is null) return null;

        foreach (var node in nodes)
        {
            if (node is not Enemy enemy) continue;

            if (enemy.Progress > bestProgress && GlobalPosition.DistanceTo(enemy.GlobalPosition) <= _turretRange)
            {
                bestTarget = enemy;
                bestProgress = enemy.Progress;
            }
        }

        return bestTarget;
    }

    private void OnTimerTimeout()
    {
        if (_target is null) return;
        if (_barrel is null) return;

        var shot = _projectile?.Instantiate<Projectile>();
        if (shot is null) return;

        AddChild(shot);
        shot.GlobalPosition = _barrel.GlobalPosition;
        shot.Direction = GlobalBasis.Z;
    }
}