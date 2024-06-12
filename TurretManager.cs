using BarbarianBlaster.turret;
using Godot;

namespace BarbarianBlaster;

public partial class TurretManager : Node3D
{
    [Export] private PackedScene _turret;

    public void BuildTurret(Vector3 turretPosition)
    {
        var turret = _turret.Instantiate<Turret>();
        if (turret is null) return;

        AddChild(turret);
        turret.GlobalPosition = turretPosition;
    }
}