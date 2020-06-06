extends Area2D

const speed = 150
var movement = Vector2()
var direction = 1
export (int) var damage = 3

func set_fireball_direction(dir):
	direction = dir
	if dir == -1:
		$AnimatedSprite.flip_h = true

func _physics_process(delta):
	movement.x = speed * delta * direction
	translate(movement)
	$AnimatedSprite.play("shoot")

func _on_VisibilityNotifier2D_screen_exited():
	queue_free()

func _on_Fireball_body_entered(body):
	if "GreenGlobin" in body.name:
		body.take_damage(damage)
	elif "Mushroom" in body.name:
		body.take_damage(damage)
	elif "EyeMonster" in body.name:
		body.take_damage(damage)
	queue_free()
