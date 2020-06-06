extends CanvasLayer

var bar_red = preload("res://Assets/UI/barHorizontal_red_mid 200.png")
var bar_yellow = preload("res://Assets/UI/barHorizontal_yellow_mid 200.png")
var bar_green = preload("res://Assets/UI/barHorizontal_green_mid 200.png")
var bar_texture
var total_pie = 0
var current_health = 0
var current_level = 1

var collectSFX = AudioStreamPlayer.new()

func _ready():
	OS.set_window_position(OS.get_screen_size()*0.5 - OS.get_window_size()*0.5)
	
	self.add_child(collectSFX)
	collectSFX.stream = load("res://Assets/Audio/collect.wav")
	
func update_level(value):
	current_level = value

func update_healthbar(value):
	current_health = value
	bar_texture = bar_green
	if value < 60:
		bar_texture = bar_yellow
	if value < 25:
		bar_texture = bar_red
	$MarginContainer/HBoxContainer/HealthBar.texture_progress = bar_texture
	$MarginContainer/HBoxContainer/HealthBar/Tween.interpolate_property(
		$MarginContainer/HBoxContainer/HealthBar, 'value', 
		$MarginContainer/HBoxContainer/HealthBar.value, value,
		0.2, Tween.TRANS_LINEAR, Tween.EASE_IN_OUT)
	$MarginContainer/HBoxContainer/HealthBar/Tween.start()
	$AnimationPlayer.play("healthbar_flash")
	#$MarginContainer/HBoxContainer/HealthBar.value = value

func _on_AnimationPlayer_animation_finished(anim_name):
	if anim_name == "healthbar_flash":
		$MarginContainer/HBoxContainer/HealthBar.texture_progress = bar_texture


func _on_Player_butterChicken_collected():
	total_pie += 1
	$MarginContainer/HBoxContainer2/ButterChicken.set_self_modulate("ffffff")
	collectSFX.play()

func _on_Player_mincePie_collected():
	total_pie += 1
	$MarginContainer/HBoxContainer2/MincePie.set_self_modulate("ffffff")
	collectSFX.play()

func _on_Player_steacknCheese_collected():
	total_pie += 1
	$MarginContainer/HBoxContainer2/SteacknCheese.set_self_modulate("ffffff")
	collectSFX.play()

func _release_pies():
	$MarginContainer/HBoxContainer2/ButterChicken.set_self_modulate("202020")
	$MarginContainer/HBoxContainer2/MincePie.set_self_modulate("202020")
	$MarginContainer/HBoxContainer2/SteacknCheese.set_self_modulate("202020")
	total_pie = 0

