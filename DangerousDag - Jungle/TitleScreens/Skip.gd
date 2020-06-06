extends Button

var skip_to_scene #scene to skip to

#Custom method called by StoryBox parent
func _loadSkipToScene(scene):
	skip_to_scene = scene
	pass

#Button press action when pressed
func _pressed():
	get_tree().change_scene(skip_to_scene)
	pass
