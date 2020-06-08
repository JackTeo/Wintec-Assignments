extends KinematicBody2D

signal shoot
signal health_changed
signal dead

export (PackedScene) var Bullet
export (int) var speed
export (int) var health
export (float) var gun_cooldown

var velocity = Vector2()
var can_shoot = true
var alive = true

func _ready():
	$GunTimer.wait_time = gun_cooldown

func _physics_process(delta):
	var motion = velocity.normalized() * speed
	move_and_slide(motion, velocity)

func shoot():
	if can_shoot:
		can_shoot = false
		$GunTimer.start()
		var dir = Vector2(1, 0).rotated($Body.global_rotation)
		emit_signal('shoot', Bullet, $Body/Muzzle.global_position, dir)


func _on_GunTimer_timeout():
	can_shoot = true
