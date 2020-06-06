extends "res://Characters/Character.gd"

var direction = 1
var damage2SFX = AudioStreamPlayer.new()

func _ready():
	health = max_health
	self.add_child(damage2SFX)
	damage2SFX.stream = load("res://Assets/Audio/damage2.wav")

func _physics_process(delta):
	if is_hurt == false:
		if is_dead == false && is_hurt == false:
			movement.x = speed * direction
			if direction == 1:
				$AnimatedSprite.flip_h = false
			else:
				$AnimatedSprite.flip_h = true
			$AnimatedSprite.play("run")
			
	#		if is_on_wall():
	#			direction *= -1
	#			$RayCast2D.position.x *= -1
			
		if $RayCast2D.is_colliding() == false || $RayCast2DFront.is_colliding():
			direction = direction * -1
			$RayCast2D.position.x *= -1
			$RayCast2DFront.rotation_degrees *= -1
	else:
		movement.x = 0
	
	if get_slide_count() > 0:
		for i in range(get_slide_count()):
			if "Player" in get_slide_collision(i).collider.name:
				get_slide_collision(i).collider.take_damage(damage)

func take_damage(amount):
	if is_dead == false:
		is_hurt = true
		$AnimatedSprite.play("takehit")
		damage2SFX.play()
		health -= amount
		if health <= 0:
			dead()
		emit_signal('health_changed', health * 100 / max_health)
		yield(get_tree().create_timer(.9),"timeout")
		is_hurt = false
		
	
