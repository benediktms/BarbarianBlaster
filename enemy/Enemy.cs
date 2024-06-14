using System;
using Godot;

namespace BarbarianBlaster;

public partial class Enemy : PathFollow3D
{
    [Export] private float _speed = 5;

    private Base? _homeBase;

    public override void _Ready()
    {
        _homeBase = GetTree().GetFirstNodeInGroup("base") as Base;
    }

    public override void _Process(double delta)
    {
        Progress += (float)delta * _speed;
        if (Math.Abs(ProgressRatio - 1.0) < 0.01)
        {
            _homeBase?.TakeDamage();
            SetProcess(false);
        }
    }
}