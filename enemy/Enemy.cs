using System;
using Godot;

namespace BarbarianBlaster;

public partial class Enemy : PathFollow3D
{
    private Base _homeBase = null!;

    private AnimationPlayer _animationPlayer = null!;

    private Bank _bank = null!;

    public int? MaxHealth { get; set; }

    private int _currentHealth;

    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            if (value < _currentHealth)
            {
                _animationPlayer.Play("TakeDamage");
            }

            _currentHealth = value;

            if (_currentHealth <= 0)
            {
                _bank.Gold += _reward;
                QueueFree();
            }
        }
    }

    private int _reward = 15;

    private float _speed = 5;

    public override void _Ready()
    {
        // Engine.TimeScale = 3;
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _homeBase = (Base)GetTree().GetFirstNodeInGroup("base");
        _bank = (Bank)GetTree().GetFirstNodeInGroup("bank");

        CurrentHealth = MaxHealth ?? 3; // MaxHealth should always be set in EnemyPath
    }

    public override void _Process(double delta)
    {
        Progress += (float)delta * _speed;
        if (Math.Abs(ProgressRatio - 1.0) < 0.01)
        {
            _homeBase.TakeDamage();
            SetProcess(false);
            QueueFree();
        }
    }
}