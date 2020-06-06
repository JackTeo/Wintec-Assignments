extends "res://Characters/Character.gd"

signal butterChicken_collected
signal mincePie_collected
signal steacknCheese_collected

const JUMP_FORCE = 200
const FIREBALL = preload("res://Characters/Fireball.tscn")
var is_attacking = false

var shootSFX = AudioStreamPlayer.new()

func _ready():
	self.add_child(shootSFX)
	shootSFX.stream = load("res://Assets/Audio/Shoot.wav")

func _physics_process(delta):
	if is_dead == false:
		#controls
		if Input.is_action_pressed("ui_right"):
			if is_attacking == false || is_on_floor() == false:
				movement.x = speed
				if is_attacking == false:
					$AnimatedSprite.flip_h = false
					$AnimatedSprite.play("run")
					if sign($Position2D.position.x) == -1:
						$Position2D.position.x *= -1
		elif Input.is_action_pressed("ui_left"):
			if is_attacking == false || is_on_floor() == false:
				movement.x = -speed
				if is_attacking == false:
					$AnimatedSprite.flip_h = true
					$AnimatedSprite.play("run")
					if sign($Position2D.position.x) == 1:
						$Position2D.position.x *= -1
		else:
			if movement.x > 0:
				movement.x -= 1
			movement.x = 0
			if is_on_floor() && is_attacking == false:
				$AnimatedSprite.play("idle")
	
		if is_on_floor():
			if Input.is_action_just_pressed("ui_up") && is_attacking == false:
				movement.y = -JUMP_FORCE
		else:
			if is_attacking == false:
				if movement.y < 0:
					$AnimatedSprite.play("jump")
				else:
					$AnimatedSprite.play("fall")
	
		if Input.is_action_just_pressed("ui_select") && is_attacking == false:
			if is_on_floor():
				movement.x = 0
			is_attacking = true
			$AnimatedSprite.play("fire")
			shootSFX.play()
			var fireball = FIREBALL.instance()
			if sign($Position2D.position.x) == 1:
				fireball.set_fireball_direction(1)
			else:
				fireball.set_fireball_direction(-1)
			get_parent().add_child(fireball)
			fireball.position = $Position2D.global_position
		
		if get_slide_count() > 0:
			for i in range(get_slide_count()):
				if "GreenGlobin" in get_slide_collision(i).collider.name:
					take_damage(get_parent().get_node("GreenGlobin").damage)
				elif "Mushroom" in get_slide_collision(i).collider.name:
					take_damage(get_parent().get_node("Mushroom").damage)
				elif "EyeMonster" in get_slide_collision(i).collider.name:
					take_damage(get_parent().get_node("EyeMonster").damage)

func _on_AnimatedSprite_animation_finished():
	is_attacking = false

func _on_Timer_timeout():
	get_tree().change_scene("res://TitleScreens/Continue.tscn")

