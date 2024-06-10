@tool
class_name Base
extends Node3D

@export var max_health := 10
var current_health: int:
	set(new_value):
		current_health = new_value
		label_3d.text = str(new_value) + "/" + str(max_health)
		label_3d.modulate = Color.RED.lerp(Color.WHITE, float(current_health) / float(max_health))
		if current_health < 1:
			get_tree().reload_current_scene()
	get:
		return current_health

@onready var label_3d: Label3D = $Label3D

var path: Path3D
var target_pos: Vector3

func _ready() -> void:
	current_health = max_health

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
	current_health -= 1
