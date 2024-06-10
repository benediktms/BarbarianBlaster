@tool
class_name Base
extends Node3D

var path: Path3D
var target_pos: Vector3

func _enter_tree() -> void:
	path = get_parent() as Path3D
	if path:
		target_pos = path.curve.get_point_position(path.curve.point_count - 1) as Vector3
	else:
		update_configuration_warnings()

func _get_configuration_warnings() -> PackedStringArray:
	if path:
		return []
	else:
		return ["Must be a child of a Path3D node"]

func _process(_delta: float) -> void:
	if (Engine.is_editor_hint() and !!path and !!target_pos) and global_position != target_pos:
		global_position = path.curve.get_point_position(path.curve.point_count - 1)

func take_damage() -> void:
	print("taking damage")
