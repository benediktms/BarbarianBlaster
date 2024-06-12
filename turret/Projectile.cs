using Godot;

namespace BarbarianBlaster.turret;

public partial class Projectile : Area3D
{
    private float _speed = 30;
    private Vector3 _direction = Vector3.Forward;
    private Timer _timer;

    public override void _Ready()
    {
        base._Ready();

        _timer = GetNode<Timer>("Timer");
        _timer.OneShot = true;
        _timer.WaitTime = 2;
        _timer.Start();
        _timer.Timeout += OnTimerTimeout;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Position += _direction * _speed * (float)delta;
    }

    private void OnTimerTimeout()
    {
        QueueFree();
    }
}