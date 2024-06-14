using BarbarianBlaster.turret;
using Godot;

namespace BarbarianBlaster;

public partial class TurretManager : Node3D
{
    [Export] private PackedScene? _turret;
    [Export] private Path3D? _path;

    public void BuildTurret(Vector3 turretPosition)
    {
        var turret = _turret?.Instantiate<Turret>();
        if (turret is null) return;

        AddChild(turret);
        turret.Path = _path;
        turret.GlobalPosition = turretPosition;
    }
}