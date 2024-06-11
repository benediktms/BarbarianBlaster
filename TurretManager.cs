using Godot;

namespace BarbarianBlaster;

public partial class TurretManager : Node3D
{
    [Export] private PackedScene _turret;

    public void BuildTurret(Vector3 turretPosition)
    {
        var node = _turret.Instantiate();
        if (node is not Node3D newTurret) return;

        AddChild(newTurret);
        newTurret.GlobalPosition = turretPosition;
    }
}