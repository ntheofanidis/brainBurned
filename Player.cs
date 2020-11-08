using Godot;
using System;

public class Player : Area2D
{
	[Export]
	public int Speed = 400;
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
		
		Position += velocity * delta;
	}
}
