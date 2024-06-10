extends PathFollow3D

@export var speed := 2.0

@onready var base: Base = get_tree().get_first_node_in_group("base")

func _process(delta: float) -> void:
	progress += (speed * delta)
	if progress_ratio == 1.0:
		base.take_damage()
