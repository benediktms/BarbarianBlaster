using Godot;
using System;

namespace BarbarianBlaster;

public partial class Bank : MarginContainer
{
    private Label? _label;

    private Label Label
    {
        get
        {
            if (_label is not null) return _label;

            _label = GetNode<Label>("Label");
            return _label;
        }
        set => _label = value;
    }

    private int _gold;

    public int Gold
    {
        get => _gold;
        set
        {
            _gold = Math.Max(value, 0);
            Label.Text = $"Gold: {_gold}";
        }
    }

    public override void _Ready()
    {
        base._Ready();

        Gold = 150;
    }
}