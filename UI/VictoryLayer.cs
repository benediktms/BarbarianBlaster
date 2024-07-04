using Godot;


namespace BarbarianBlaster.UI;

public partial class VictoryLayer : CanvasLayer
{
    [Export] private Bank _bank = null!;

    private Base _base = null!;

    private Button _retryButton = null!;
    private Button _quitButton = null!;

    private Label _healthLabel = null!;
    private Label _moneyLabel = null!;

    private TextureRect _star1 = null!;
    private TextureRect _star2 = null!;
    private TextureRect _star3 = null!;

    public override void _Ready()
    {
        Visible = false;

        _base = (Base)GetTree().GetFirstNodeInGroup("base");

        _retryButton = GetNode<Button>("%RetryButton");
        _quitButton = GetNode<Button>("%QuitButton");

        _healthLabel = GetNode<Label>("%HealthLabel");
        _healthLabel.Visible = false;

        _moneyLabel = GetNode<Label>("%MoneyLabel");
        _moneyLabel.Visible = false;

        _star1 = GetNode<TextureRect>("%Star1");
        _star2 = GetNode<TextureRect>("%Star2");
        _star3 = GetNode<TextureRect>("%Star3");

        _star2.Modulate = Colors.Black;
        _star3.Modulate = Colors.Black;

        _retryButton.Pressed += RestartGame;
        _quitButton.Pressed += QuitGame;
    }

    public void Victory()
    {
        Visible = true;
        if (_base.MaxHealth == _base.CurrentHealth)
        {
            _star2.Modulate = Colors.White;
            _healthLabel.Visible = true;
        }

        if (_bank.Gold >= 500)
        {
            _star3.Modulate = Colors.White;
            _moneyLabel.Visible = true;
        }

        {
        }
    }

    private void RestartGame()
    {
        GetTree().ReloadCurrentScene();
    }

    private void QuitGame()
    {
        GetTree().Quit();
    }
}