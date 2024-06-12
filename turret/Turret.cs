using Godot;

namespace BarbarianBlaster.turret;

public partial class Turret : Node3D
{
    [Export] private PackedScene _projectile;
    private Timer _timer;
    private MeshInstance3D _barrel;

    public override void _Ready()
    {
        base._Ready();

        _timer = GetNode<Timer>("Timer");
        _timer.Timeout += OnTimerTimeout;
        _timer.Start();

        _barrel = GetNode<MeshInstance3D>("TurretBase/TurretTop/Barrel");
    }

    private void OnTimerTimeout()
    {
        var shot = _projectile.Instantiate<Projectile>();
        if (shot is null) return;

        AddChild(shot);
        shot.GlobalPosition = _barrel.GlobalPosition;
    }
}