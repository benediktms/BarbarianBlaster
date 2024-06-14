using Godot;

namespace BarbarianBlaster;

public partial class Base : Node
{
    private Label3D? _label;

    [Export] private int _maxHealth = 6;
    private int _currentHealth;
    private int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            _label.Text = value + "/" + _maxHealth;
            _label.Modulate = Colors.Red.Lerp(Colors.White, (float)CurrentHealth / _maxHealth);

            if (CurrentHealth < 1) GetTree().ReloadCurrentScene();
        }
    }

    public override void _Ready()
    {
        _label = GetNode<Label3D>("Label3D");
        CurrentHealth = _maxHealth;
    }

    public void TakeDamage()
    {
        CurrentHealth -= 1;
    }
}
