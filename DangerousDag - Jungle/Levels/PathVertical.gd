extends Path2D

onready var follow = get_node("PathFollow2D")
export (int) var speed = 25

func _ready():
	set_process(true)

func _process(delta):
	follow.set_offset(follow.get_offset() + speed * delta)
