extends Camera3D

@onready var ray_cast: RayCast3D = $RayCast3D

func _process(_delta: float) -> void:
	var mouse_position := get_viewport().get_mouse_position()
	ray_cast.target_position = project_local_ray_normal(mouse_position) * 100.0
	ray_cast.force_raycast_update()

	if not ray_cast.is_colliding() or not ray_cast.get_collider() is GridMap: return
	var grid_map := ray_cast.get_collider() as GridMap
	var collision_point := grid_map.to_local(ray_cast.get_collision_point())
	var cell := grid_map.local_to_map(collision_point)

	if grid_map.get_cell_item(cell) == 0:
		grid_map.set_cell_item(cell, 1)
