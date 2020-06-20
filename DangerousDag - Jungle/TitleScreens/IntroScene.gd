extends Node

export (String, FILE,"*.tscn") var skip_to_scene
#export (String, FILE,"*.txt") var story_line_file
export (Texture) var person

#onready var stsNode = get_node("Skip") #to skip to next world
onready var cbNode = get_node("Panel/ChatBox") #to add our story
onready var pNode = get_node("Panel/Person") #to add a person sprite

# Called when the node enters the scene tree for the first time.
func _ready():
	$Panel/HBoxContainer/TextureButton.grab_focus()
	cbNode.loadSkipToScene(skip_to_scene)
#	cbNode.loadDialogFromFile(story_line_file)
	pNode.texture = person
#	pass # Replace with function body.


#func _physics_process(delta):
#	if $Panel/HBoxContainer/TextureButton.is_hovered() == true:
#		$Panel/HBoxContainer/TextureButton.grab_focus()
#	if $Panel/HBoxContainer/TextureButton2.is_hovered() == true:
#		$Panel/HBoxContainer/TextureButton2.grab_focus()
