extends Node

func _ready():
	get_node("/root/HUD").layer = -10
	$MarginContainer/VBoxContainer/VBoxContainer/TextureButton.grab_focus()
	var player = AudioStreamPlayer.new()
	self.add_child(player)
	player.stream = load("res://Assets/Audio/bgm_title.wav")
	player.play()
	get_node("/root/HUD").update_level(1)

func _physics_process(delta):
	if $MarginContainer/VBoxContainer/VBoxContainer/TextureButton.is_hovered() == true:
		$MarginContainer/VBoxContainer/VBoxContainer/TextureButton.grab_focus()
	if $MarginContainer/VBoxContainer/VBoxContainer/TextureButton2.is_hovered() == true:
		$MarginContainer/VBoxContainer/VBoxContainer/TextureButton2.grab_focus()


func _on_TextureButton_pressed():
	get_node("/root/HUD").update_healthbar(100)
	get_tree().change_scene("res://TitleScreens/IntroScene.tscn")


func _on_TextureButton2_pressed():
	get_tree().quit()
