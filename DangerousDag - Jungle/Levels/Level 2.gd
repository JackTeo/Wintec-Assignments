extends Node2D

var play_once = false
var repeat = false

func _ready():
	set_camera_limits()
	var player = AudioStreamPlayer.new()
	self.add_child(player)
	player.stream = load("res://Assets/Audio/bgm_level2.wav")
	player.play()

func _physics_process(delta):
	if get_node("/root/HUD").total_pie >= 3 && play_once == false:
		$Portal.visible = true
		$Portal.set_collision_layer_bit(0, true)
		$Portal/AnimatedSprite.play("emerge")
		yield(get_tree().create_timer(.6),"timeout")
		$Portal/AnimatedSprite.play("idle")
		get_node("/root/HUD").total_pie = 0
		play_once = true
	
	if repeat == true:
		get_node("Player").take_damage(1)
	
func set_camera_limits():
	var map_limits = $TileMap.get_used_rect()
	var map_cellsize = $TileMap.cell_size
	$Player/Camera2D.limit_left = 0 #map_limits.position.x * map_cellsize.x
	$Player/Camera2D.limit_right = 710 #map_limits.end.x * map_cellsize.x
	$Player/Camera2D.limit_top = 0 #map_limits.position.y * map_cellsize.y
	$Player/Camera2D.limit_bottom = 220 #map_limits.end.y * map_cellsize.y

func _on_Area2D_body_entered(body):
	if "Player" in body.name:
		body.take_damage(1)
		repeat = true

func _on_Area2D_body_exited(body):
	repeat = false
