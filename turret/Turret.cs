using Godot;

namespace BarbarianBlaster.turret;

public partial class Turret : Node3D
{
    [Export] private PackedScene? _projectile;

    public Path3D? Path { get; set; }

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

    private MeshInstance3D? _barrel;

    private MeshInstance3D Barrel
    {
        get
        {
            if (_barrel is not null) return _barrel;
            _barrel = GetNode<MeshInstance3D>("TurretBase/TurretTop/Barrel");
            return _barrel;
        }
    }

    private AnimationPlayer? _animationPlayer;

    private AnimationPlayer AnimationPlayer
    {
        get
        {
            if (_animationPlayer is not null) return _animationPlayer;
            _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
            return _animationPlayer;
        }
    }

    private Enemy? _target;

    private float _turretRange = 10;

    public override void _Ready()
    {
        base._Ready();

        Timer.Timeout += OnTimerTimeout;
        Timer.Start();
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

        var shot = _projectile?.Instantiate<Projectile>();
        if (shot is null) return;

        AddChild(shot);
        shot.GlobalPosition = Barrel.GlobalPosition;
        shot.Direction = GlobalBasis.Z;
        AnimationPlayer.Play("fire");
    }
}