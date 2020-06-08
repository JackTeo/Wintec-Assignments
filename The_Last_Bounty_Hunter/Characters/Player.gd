extends "res://Characters/Character.gd"

func _physics_process(delta):
	var UP = Input.is_action_pressed("ui_up")
	var DOWN = Input.is_action_pressed("ui_down")
	var LEFT = Input.is_action_pressed("ui_left")
	var RIGHT = Input.is_action_pressed("ui_right")
	
	$Body.look_at(get_global_mouse_position())
	
	velocity.x = -int(LEFT) + int(RIGHT)
	velocity.y = -int(UP) + int(DOWN)
	
	if Input.is_action_pressed("left_click"):
		shoot()
