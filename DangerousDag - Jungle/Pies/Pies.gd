extends Area2D

var movement = Vector2()
var stop = false

func _physics_process(delta):
	$AnimatedSprite.play("idle")
	movement.y += 8 * delta
	if stop == false:
		translate(movement)


#func _on_body_entered(body):
#	if "EyeMonster" in body.name || "GreenGlobin" in body.name || "Mushroom" in body.name:
#		stop = false
#	else:
#		stop = true
#	if "Player" in body.name:
#		if "ButterChicken" in body.name:
#			body.emit_signal('butterChicken_collected')
#		elif "MincePie" in body.name:
#			body.emit_signal('mincePie_collected')
#		elif "SteacknCheese" in body.name:
#			body.emit_signal('steacknCheese_collected')
#		queue_free()

