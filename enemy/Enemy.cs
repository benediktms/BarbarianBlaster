using System;
using Godot;

namespace BarbarianBlaster;

public partial class Enemy : PathFollow3D
{
    [Export] private float _speed = 5;

    private Base? _homeBase;

    private Base HomeBase
    {
        get
        {
            if (_homeBase is not null) return _homeBase;
            _homeBase = (Base)GetTree().GetFirstNodeInGroup("base");
            return _homeBase;
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

    private int _reward = 15;
    private Bank? _bank;

    private Bank Bank
    {
        get
        {
            if (_bank is not null) return _bank;

            _bank = (Bank)GetTree().GetFirstNodeInGroup("bank");
            return _bank;
        }
    }

    public int? MaxHealth { get; set; }

    private int _currentHealth;

    [Export]
    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            if (value < _currentHealth)
            {
                AnimationPlayer.Play("TakeDamage");
            }

            _currentHealth = value;

            if (_currentHealth <= 0)
            {
                Bank.Gold += _reward;
                QueueFree();
            }
        }
    }

    public override void _Ready()
    {
        CurrentHealth = MaxHealth ?? 3; // MaxHealth should always be set in EnemyPath
    }

    public override void _Process(double delta)
    {
        Progress += (float)delta * _speed;
        if (Math.Abs(ProgressRatio - 1.0) < 0.01)
        {
            HomeBase.TakeDamage();
            SetProcess(false);
        }
    }
}