using System.Linq;
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

        if (Path?.GetChildren().Last() is not Enemy enemy) return;

        LookAt(enemy.GlobalPosition, Vector3.Up, true);
    }

    private void OnTimerTimeout()
    {
        if (_barrel is null) return;

        var shot = _projectile?.Instantiate<Projectile>();
        if (shot is null) return;

        AddChild(shot);
        shot.GlobalPosition = _barrel.GlobalPosition;
        shot.Direction = GlobalBasis.Z;
    }
}