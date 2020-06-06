extends "res://Characters/Enemy.gd"

const PIE = preload("res://Pies/MincePie.tscn")
var run_once = false

func _physics_process(delta):
	if is_dead == true && run_once == false:
		var pie = PIE.instance()
		get_parent().add_child(pie)
		pie.position = $Position2D.global_position
		run_once = true
