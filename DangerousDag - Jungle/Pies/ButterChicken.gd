extends "res://Pies/Pies.gd"

func _on_body_entered(body):
	if "EyeMonster" in body.name || "GreenGlobin" in body.name || "Mushroom" in body.name:
		stop = false
	else:
		stop = true
	if "Player" in body.name:
		get_node("/root/HUD")._on_Player_butterChicken_collected()
		queue_free()
