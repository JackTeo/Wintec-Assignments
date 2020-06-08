extends Node

const center = Vector2(0, 0)
const left = Vector2(-1, 0)
const right = Vector2(1, 0)
const up = Vector2(0, -1)
const down = Vector2(0, 1)

func rand():
	var d = randi() % 5 + 1 # random number max value of 4, min of 1
	match d:
		1:
			return left
		2:
			return right
		3:
			return up
		4:
			return down
		5:
			return center
