using Godot;

namespace BarbarianBlaster.turret;

public partial class Projectile : Area3D
{
    private float _speed = 30;
    public Vector3 Direction = Vector3.Forward;

    private Timer? _timer;

    public override void _Ready()
    {
        base._Ready();

        _timer = GetNode<Timer>("Timer");
        _timer.OneShot = true;
        _timer.WaitTime = 2;
        _timer.Start();
        _timer.Timeout += OnTimerTimeout;

        AreaEntered += OnAreaEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Position += Direction * _speed * (float)delta;
    }

    private void OnTimerTimeout()
    {
        QueueFree();
    }

    private void OnAreaEntered(Area3D area)
    {
        if (!area.IsInGroup("enemy_area")) return;

        var enemy = area.GetParent<Enemy>();
        if (enemy is null)
        {
            GD.PushError($"Parent of enemy area {area.GetParent()} is of unexpected type");
            return;
        }

        enemy.CurrentHealth -= 1;
        QueueFree();
    }
}