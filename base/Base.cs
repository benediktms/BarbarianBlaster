using Godot;

namespace BarbarianBlaster;

public partial class Base : Node
{
    private Label3D? _label;

    private int _maxHealth = 15;
    public int MaxHealth { get; set; }

    private int _currentHealth;

    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;

            if (_label is not null)
            {
                _label.Text = $"{value}/{_maxHealth}";
                _label.Modulate = Colors.Red.Lerp(Colors.White, (float)CurrentHealth / _maxHealth);
            }

            if (CurrentHealth < 1) GetTree().ReloadCurrentScene();
        }
    }

    public override void _Ready()
    {
        _label = GetNode<Label3D>("Label3D");
        MaxHealth = _maxHealth;
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage()
    {
        CurrentHealth -= 1;
    }
}