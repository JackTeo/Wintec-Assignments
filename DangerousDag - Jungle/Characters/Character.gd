extends KinematicBody2D

signal health_changed
signal dead

export (int) var speed
export (int) var max_health
export (int) var damage

var movement = Vector2()
const FLOOR = Vector2(0, -1)
const GRAVITY = 10
var is_dead = false
var snap = Vector2.DOWN * 2
var health
var is_hurt = false

var dieSFX = AudioStreamPlayer.new()
var damageSFX = AudioStreamPlayer.new()

func _ready():
	if get_node("/root/HUD").current_health == 0:
		health = max_health
		get_node("/root/HUD").update_healthbar(health * 100/max_health)
	else:
		health = get_node("/root/HUD").current_health / 10
	
	self.add_child(dieSFX)
	dieSFX.stream = load("res://Assets/Audio/die.wav")
	self.add_child(damageSFX)
	damageSFX.stream = load("res://Assets/Audio/Damage.wav")

func _physics_process(delta):
	movement.y += GRAVITY
	movement = move_and_slide_with_snap(movement, snap, FLOOR)
	
func take_damage(amount):
	if is_dead == false:
		if $InvulnerableTimer.is_stopped():
			$InvulnerableTimer.start()
			$AnimatedSprite.play("takehit")
			damageSFX.play()
			health -= amount
			get_node("/root/HUD").update_healthbar(health * 100/max_health)
		#	knockback()
		#	$AnimatedSprite.stop()
			set_collision_layer_bit(1, false)
			set_collision_mask_bit(1, false)
			if health <= 0:
				dead();
		yield(get_tree().create_timer($InvulnerableTimer.wait_time),"timeout")
		set_collision_layer_bit(1, true)
		set_collision_mask_bit(1, true)

func dead():
	is_dead = true
	movement = Vector2(0, 0)
#	#$CollisionShape2D.set_deferred("disabled", true)
#	#$CollisionShape2D.free()
	set_collision_layer_bit(1, false)
	set_collision_mask_bit(1, false)
	$AnimatedSprite.play("death")
	dieSFX.play()
	$Timer.start()
#	get_parent().get_node("ScreenShake").screen_shake(1, 10, 100)

func knockback():
#	for body in $Hitbox.get_overlapping_bodies():
		movement.y = -100

func _on_Timer_timeout():
	queue_free()
