extends Area2D

export(String, FILE, "*.tscn") var next_stage



func _on_Portal_body_entered(body):
	if "Player" in body.name:
		get_tree().change_scene(next_stage)
		get_node("/root/HUD")._release_pies()
	get_node("/root/HUD").update_level(2)

#func _physics_process(delta):
#	$AnimatedSprite.play("idle")
