using System;
using Godot;

namespace BarbarianBlaster;

public partial class Enemy : PathFollow3D
{
    [Export] private float _speed = 5;

    private Base? _homeBase;

    private AnimationPlayer? _animationPlayer;

    private int _maxHealth = 3;
    private int _currentHealth;

    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            if (value < _currentHealth)
            {
                _animationPlayer?.Play("TakeDamage");
            }

            _currentHealth = value;

            if (value <= 0) QueueFree();
        }
    }

    public override void _Ready()
    {
        _homeBase = (Base)GetTree().GetFirstNodeInGroup("base");
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

        CurrentHealth = _maxHealth;
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