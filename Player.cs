using Godot;
using System;

public class Player : KinematicBody2D
{
	[Export]
	public int Speed = 1400;
	private Vector2 _screenSize;


	public override void _Ready()
	{
		_screenSize = GetViewport().Size;
	}

	public override void _Process(float delta)
	{
		var velocity = new Vector2();
		
		velocity.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		velocity.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");

		var animatedSprite = GetNode<AnimatedSprite>("PlayerSprite");

		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			animatedSprite.Play();
		}
		else
		{
			animatedSprite.Stop();
		}
		
		if (velocity.x != 0)
		{
			animatedSprite.FlipH = velocity.x > 0;
		}
		
		velocity = MoveAndSlide(velocity, new Vector2(0, -1));
	}
}
