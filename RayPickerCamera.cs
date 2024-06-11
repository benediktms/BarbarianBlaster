using Godot;

namespace BarbarianBlaster;

public partial class RayPickerCamera : Camera3D
{
    [Export] private TurretManager _turretManager;

    private RayCast3D _rayCast;

    public override void _Ready()
    {
        base._Ready();

        _rayCast = GetNode<RayCast3D>("RayCast3D");

        if (_turretManager is null)
        {
            GD.PrintErr("Turret Manager not set");
        }
    }

    public override void _Process(double delta)
    {
        _rayCast.TargetPosition = ProjectLocalRayNormal(_rayCast.GetViewport().GetMousePosition()) * 100;
        _rayCast.ForceRaycastUpdate();
        if (!_rayCast.IsColliding())
        {
            Input.SetDefaultCursorShape();
        }

        var gridMap = CastToGridMap(_rayCast.GetCollider());
        if (gridMap is null) return;

        if (!Input.IsActionPressed("click")) return;
        
        var collisionPoint = gridMap.ToLocal(_rayCast.GetCollisionPoint());
        var cell = gridMap.LocalToMap(collisionPoint);

        if (gridMap.GetCellItem(cell) != 0) return;
        if (_turretManager is null)
        {
            GD.PrintErr("Can't build turret. Turret manager is not set");
            return;
        }

        gridMap.SetCellItem(cell, 1);
        _turretManager.BuildTurret(gridMap.MapToLocal(cell));
    }

    private static GridMap CastToGridMap(GodotObject target)
    {
        if (target is not GridMap gridMap) return null;

        Input.SetDefaultCursorShape(Input.CursorShape.PointingHand);
        return gridMap;
    }
}